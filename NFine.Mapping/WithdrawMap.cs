//-----------------------------------------------------------------------
// <copyright file=" Withdraw.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Withdraw.cs
// * history : Created by T4 01/29/2019 15:52:00 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.Withdraw
{
    public class WithdrawMap : EntityTypeConfiguration<WithdrawEntity>
    {
		 public WithdrawMap()
        {
            this.ToTable("Sys_Withdraw");
            this.HasKey(t => t.F_Id);
        }
    }
}