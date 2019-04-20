//-----------------------------------------------------------------------
// <copyright file=" WealthLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: WealthLog.cs
// * history : Created by T4 01/29/2019 15:51:38 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.IRepository.WealthLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.WealthLog
{
    public class WealthLogRepository : RepositoryBase<WealthLogEntity>, IWealthLogRepository
    {
    }
}