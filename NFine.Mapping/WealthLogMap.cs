//-----------------------------------------------------------------------
// <copyright file=" WealthLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: WealthLog.cs
// * history : Created by T4 01/29/2019 15:51:37 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.WealthLog
{
    public class WealthLogMap : EntityTypeConfiguration<WealthLogEntity>
    {
		 public WealthLogMap()
        {
            this.ToTable("Sys_WealthLog");
            this.HasKey(t => t.F_Id);
        }
    }
}