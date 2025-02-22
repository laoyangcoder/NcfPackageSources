﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET;
using Senparc.Ncf.Core.Cache;
using Senparc.Ncf.Core.DI;
using Senparc.Ncf.Core.Enums;
using Senparc.Ncf.Core.Exceptions;
using Senparc.Ncf.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Senparc.Ncf.Core.Cache
{
    [AutoDIType(DILifecycleType.Scoped)]
    public class FullSystemConfigCache : BaseCache<FullSystemConfig>/*, IFullSystemConfigCache*/
    {
        public const string CACHE_KEY = "FullSystemConfigCache";
        private INcfDbData _dataContext => base._db as INcfDbData;

        public FullSystemConfigCache(INcfDbData db)
            : base(CACHE_KEY, db)
        {
            base.TimeOut = 1440;
        }

        public override FullSystemConfig Update()
        {
            SystemConfig systemConfig = null;
            try
            {
                systemConfig = (_dataContext.BaseDataContext as SenparcEntitiesBase).Set<SystemConfig>().FirstOrDefault();
            }
            catch
            {
                new NcfUninstallException("FullSystemConfigCache 访问数据库异常，推测系统未安装或未正确配置数据库");
            }

            FullSystemConfig fullSystemConfig;
            if (systemConfig != null)
            {
                fullSystemConfig = FullSystemConfig.CreateEntity<FullSystemConfig>(systemConfig);
            }
            else
            {
                string hostName = null;
                try
                {
                    var httpContextAccessor = SenparcDI.GetServiceProvider().GetService<IHttpContextAccessor>();
                    var httpContext = httpContextAccessor.HttpContext;
                    var urlData = httpContext.Request;
                    var scheme = urlData.Scheme;//协议
                    var host = urlData.Host.Host;//主机名（不带端口）
                    var port = urlData.Host.Port ?? -1;//端口（因为从.NET Framework移植，因此不直接使用urlData.Host）
                    string portSetting = null;//Url中的端口部分
                    string schemeUpper = scheme.ToUpper();//协议（大写）
                    string baseUrl = httpContext.Request.PathBase;//子站点应用路径

                    if (port == -1 || //这个条件只有在 .net core 中， Host.Port == null 的情况下才会发生
               (schemeUpper == "HTTP" && port == 80) ||
               (schemeUpper == "HTTPS" && port == 443))
                    {
                        portSetting = "";//使用默认值
                    }
                    else
                    {
                        portSetting = ":" + port;//添加端口
                    }

                    hostName = $"{scheme}://{host}{portSetting}";

                }
                catch
                {
                }

                //尝试安装

                throw new NcfUninstallException($"NCF 系统未初始化，请先执行 {hostName}/Install 进行数据初始化");
            }


            base.SetData(fullSystemConfig, base.TimeOut, null);
            return base.Data;
        }
    }
}
