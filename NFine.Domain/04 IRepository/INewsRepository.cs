//-----------------------------------------------------------------------
// <copyright file=" News.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: News.cs
// * history : Created by T4 01/30/2019 12:19:04 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.IRepository.News
{
    public interface INewsRepository : IRepositoryBase<NewsEntity>
    {
    }
}