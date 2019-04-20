//-----------------------------------------------------------------------
// <copyright file=" SysUserVipRanking.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: SysUserVipRanking.cs
// * history : Created by T4 03/02/2019 22:08:31 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.IRepository.UserVipRanking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.UserVipRanking
{
    public class UserVipRankingRepository : RepositoryBase<UserVipRankingEntity>, IUserVipRankingRepository
    {
    }
}