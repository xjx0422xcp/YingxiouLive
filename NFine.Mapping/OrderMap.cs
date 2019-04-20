//-----------------------------------------------------------------------
// <copyright file=" Order.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Order.cs
// * history : Created by T4 01/29/2019 15:52:36 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.Order
{
    public class OrderMap : EntityTypeConfiguration<OrderEntity>
    {
		 public OrderMap()
        {
            this.ToTable("Sys_Order");
            this.HasKey(t => t.F_Id);
            
        }
    }
}