//-----------------------------------------------------------------------
// <copyright file=" SysUserVipRanking.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: SysUserVipRanking.cs
// * history : Created by T4 03/02/2019 22:08:31 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NFine.Mapping.UserVipRanking
{
    public class UserVipRankingMap : EntityTypeConfiguration<UserVipRankingEntity>
    {
		 public UserVipRankingMap()
        {
            this.ToTable("Sys_UserVipRanking");
            this.HasKey(t => t.F_Id);
        }
    }
}