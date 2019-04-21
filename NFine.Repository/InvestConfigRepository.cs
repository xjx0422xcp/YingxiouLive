//-----------------------------------------------------------------------
// <copyright file=" InvestConfig.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: InvestConfig.cs
// * history : Created by T4 04/21/2019 10:59:45 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.InvestConfig;
using NFine.Domain.IRepository.InvestConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.InvestConfig
{
    public class InvestConfigRepository : RepositoryBase<InvestConfigEntity>, IInvestConfigRepository
    {
    }
}