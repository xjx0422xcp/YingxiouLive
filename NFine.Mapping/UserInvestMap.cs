//-----------------------------------------------------------------------
// <copyright file=" UserInvest.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserInvest.cs
// * history : Created by T4 04/24/2019 19:21:23 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity.UserInvest;
using NFine.Domain.IRepository.UserInvest;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NFine.Mapping.UserInvest
{
    public class UserInvestMap : EntityTypeConfiguration<UserInvestEntity>
    {
		 public UserInvestMap()
        {
            this.ToTable("Sys_UserInvest");
            this.HasKey(t => t.F_Id);
        }
    }
}