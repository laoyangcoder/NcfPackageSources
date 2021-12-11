﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Senparc.Xncf.Tanent.Domain.DatabaseModel;

#nullable disable

namespace Senparc.Xncf.Tenant.Domain.Migrations.Mysql
{
    [DbContext(typeof(TenantSenparcEntities_MySql))]
    [Migration("20211211105015_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Senparc.Ncf.Core.Models.DataBaseModel.TenantInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("TenantKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TenantInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
