//-----------------------------------------------------------------------
// <copyright file=" Gift.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Gift.cs
// * history : Created by T4 04/21/2019 10:43:29 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.Gift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.IRepository.Gift
{
    public interface IGiftRepository : IRepositoryBase<GiftEntity>
    {
    }
}