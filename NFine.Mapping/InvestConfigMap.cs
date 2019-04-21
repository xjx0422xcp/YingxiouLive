//-----------------------------------------------------------------------
// <copyright file=" InvestConfig.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: InvestConfig.cs
// * history : Created by T4 04/21/2019 10:59:45 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity.InvestConfig;
using NFine.Domain.IRepository.InvestConfig;
using NFine.Repository.InvestConfig;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Mapping.InvestConfig
{
    public class InvestConfigMap : EntityTypeConfiguration<InvestConfigEntity>
    {
		 public InvestConfigMap()
        {
            this.ToTable("InvestConfig");
            this.HasKey(t => t.ID);
        }
    }
}