//-----------------------------------------------------------------------
// <copyright file=" UserFans.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserFans.cs
// * history : Created by T4 04/24/2019 18:36:11 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.UserFans;
using NFine.Domain.IRepository.UserFans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.UserFans
{
    public class UserFansRepository : RepositoryBase<UserFansEntity>, IUserFansRepository
    {
    }
}