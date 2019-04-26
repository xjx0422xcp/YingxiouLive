//-----------------------------------------------------------------------
// <copyright file=" UserFans.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserFans.cs
// * history : Created by T4 04/24/2019 18:36:10 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity.UserFans;
using NFine.Domain.IRepository.UserFans;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NFine.Mapping.UserFans
{
    public class UserFansMap : EntityTypeConfiguration<UserFansEntity>
    {
		 public UserFansMap()
        {
            this.ToTable("Sys_UserFans");
            this.HasKey(t => t.F_Id);
        }
    }
}