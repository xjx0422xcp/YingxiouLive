//-----------------------------------------------------------------------
// <copyright file=" OrderItem.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: OrderItem.cs
// * history : Created by T4 01/29/2019 15:53:02 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.OrderItem
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItemEntity>
    {
        public OrderItemMap()
        {
            this.ToTable("Sys_OrderItem");
            this.HasKey(t => t.F_Id);
        }
    }
}