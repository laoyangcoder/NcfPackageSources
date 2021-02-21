﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Senparc.CO2NET;
using Senparc.Ncf.Core.DI;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Core.Models.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Ncf.Core.Cache
{
    /* 需要关注及优化：
     * 1、当前依赖注入的对象 INcfDbData 仍然会注入 SenparcEntitiesBase，
     *  而 SenparcEntitiesBase 的 OnModelCreating() 一旦被执行，会尝试读取此处的缓存，如果缓存未被初始化，就会陷入死循环。
     *  因此当前缓存内禁止调用 INcfDbData 下的查询。
     * 2、避免使用 INcfDbData，或者避免使用 BaseCache（因为 INcfDbData 默认注入对象为 NCF 模板中的 SenparcEntities，继承自 SenparcEntitiesBase）
     */

    /// <summary>
    /// 多租户信息缓存
    /// </summary>
    [AutoDIType(DILifecycleType.Scoped)]
    public class FullTenantInfoCache : BaseCache<Dictionary<string, TenantInfoDto>>
    {
        public const string CACHE_KEY = "FullTenantInfoCache";


        private IMapper _mapper;
        private readonly SenparcEntitiesMultiTenant _senparcEntitiesMultiTenantBase;
        //private SenparcEntitiesBase _senparcEntitiesBase => base._db.BaseDataContext as SenparcEntitiesBase;

        /// <summary>
        /// 获取当前启用状态的 TenantInfo
        /// </summary>
        /// <returns></returns>
        private async Task<List<TenantInfo>> GetEnabledListAsync()
        {
            List<TenantInfo> list = null;
            try
            {
                _senparcEntitiesMultiTenantBase.SetMultiTenantEnable(false);
                list = await _senparcEntitiesMultiTenantBase.TenantInfos.Where(z => z.Enable).ToListAsync();
            }
            finally
            {
                _senparcEntitiesMultiTenantBase.ResetMultiTenantEnable();
            }
            return list;
        }


        public FullTenantInfoCache(INcfDbData db, IMapper mapper, SenparcEntitiesMultiTenant senparcEntitiesMultiTenantBase)
            : this(CACHE_KEY, db, 1440)
        {
            _mapper = mapper;
            this._senparcEntitiesMultiTenantBase = senparcEntitiesMultiTenantBase;
        }

        public FullTenantInfoCache(string CACHE_KEY, INcfDbData db, int timeOut) : base(CACHE_KEY, db)
        {
            base.TimeOut = timeOut;
        }

        public override Dictionary<string, TenantInfoDto> Update()
        {
            throw new Senparc.Ncf.Core.Exceptions.NcfTenantException("FullTenantInfoCache 不支持同步更新方法，请使用异步方法 UpdateAsync()");
        }

        public override async Task<Dictionary<string, TenantInfoDto>> UpdateAsync()
        {
            List<TenantInfo> fullList = null;

            try
            {
                fullList = await GetEnabledListAsync();
            }
            catch (Exception)
            {
                //当前可能正在 Middleware 的获取过程中，还没有完成多租户获取
                //如果从旧版本升级，表不存在，则需要更新
                _senparcEntitiesMultiTenantBase.ResetMigrate();
                _senparcEntitiesMultiTenantBase.Migrate();

                //var HttpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();//注意：这里不能使用 TenantInfoService.SetScopedRequestTenantInfoAsync()， 否则会引发死循环
            }
            finally
            {
                fullList = fullList ?? await GetEnabledListAsync();
            }

            var tenantInfoCollection = new Dictionary<string, TenantInfoDto>(StringComparer.OrdinalIgnoreCase);
            fullList.ForEach(z => tenantInfoCollection[z.TenantKey.ToUpper()/*全部大写*/] = _mapper.Map<TenantInfoDto>(z));
            await base.SetDataAsync(tenantInfoCollection, base.TimeOut, null);

            return await GetDataAsync();
        }
    }
}
