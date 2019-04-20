//-----------------------------------------------------------------------
// <copyright file=" UserSign.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserSign.cs
// * history : Created by T4 03/13/2019 01:19:05 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity.UserSign;
using NFine.Domain.IRepository.UserSign;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NFine.Mapping.UserSign
{
    public class UserSignMap : EntityTypeConfiguration<UserSignEntity>
    {
        public UserSignMap()
        {
            this.ToTable("Sys_UserSign");
            this.HasKey(t => t.F_Id);
        }
    }
}