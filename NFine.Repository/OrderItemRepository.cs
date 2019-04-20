//-----------------------------------------------------------------------
// <copyright file=" OrderItem.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: OrderItem.cs
// * history : Created by T4 01/29/2019 15:53:02 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.IRepository.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.OrderItem
{
    public class OrderItemRepository : RepositoryBase<OrderItemEntity>, IOrderItemRepository
    {
    }
}