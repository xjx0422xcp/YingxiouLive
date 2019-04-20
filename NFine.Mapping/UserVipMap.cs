//-----------------------------------------------------------------------
// <copyright file=" UserVip.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserVip.cs
// * history : Created by T4 01/29/2019 15:51:02 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.UserVip
{
    public class UserVipMap : EntityTypeConfiguration<UserVipEntity>
    {
		 public UserVipMap()
        {
            this.ToTable("Sys_UserVip");
            this.HasKey(t => t.F_Id);
        }
    }
}