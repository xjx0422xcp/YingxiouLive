//-----------------------------------------------------------------------
// <copyright file=" Gift.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Gift.cs
// * history : Created by T4 04/21/2019 10:43:30 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.Gift;
using NFine.Domain.IRepository.Gift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.Gift
{
    public class GiftRepository : RepositoryBase<GiftEntity>, IGiftRepository
    {
    }
}