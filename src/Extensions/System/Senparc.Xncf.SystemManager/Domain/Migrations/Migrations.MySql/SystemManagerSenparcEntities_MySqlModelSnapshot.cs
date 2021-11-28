﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Senparc.Xncf.SystemManager.Models;

#nullable disable

namespace Senparc.Xncf.SystemManager.Domain.Migrations.Migrations.MySql
{
    [DbContext(typeof(SystemManagerSenparcEntities_MySql))]
    partial class SystemManagerSenparcEntities_MySqlModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Senparc.Ncf.Core.Models.SystemConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("HideModuleManager")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MchId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MchKey")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TenPayAppId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SystemConfigs");
                });
#pragma warning restore 612, 618
        }
    }
}
