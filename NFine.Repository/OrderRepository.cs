//-----------------------------------------------------------------------
// <copyright file=" Order.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Order.cs
// * history : Created by T4 01/29/2019 15:52:36 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.IRepository.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.Order
{
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
    {
    }
}