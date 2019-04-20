//-----------------------------------------------------------------------
// <copyright file=" UserSign.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserSign.cs
// * history : Created by T4 03/13/2019 01:19:06 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.UserSign;
using NFine.Domain.IRepository.UserSign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.UserSign
{
    public class UserSignRepository : RepositoryBase<UserSignEntity>, IUserSignRepository
    {
    }
}