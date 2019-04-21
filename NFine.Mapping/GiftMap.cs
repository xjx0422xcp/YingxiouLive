//-----------------------------------------------------------------------
// <copyright file=" Gift.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Gift.cs
// * history : Created by T4 04/21/2019 10:43:29 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity.Gift;
using NFine.Domain.IRepository.Gift;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NFine.Mapping.Gift
{
    public class GiftMap : EntityTypeConfiguration<GiftEntity>
    {
		 public GiftMap()
        {
            this.ToTable("Sys_Gift");
            this.HasKey(t => t.F_Id);
        }
    }
}