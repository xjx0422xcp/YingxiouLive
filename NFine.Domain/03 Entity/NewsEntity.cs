//-----------------------------------------------------------------------
// <copyright file=" News.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: News.cs
// * history : Created by T4 01/30/2019 12:19:03 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    /// <summary>
    /// News Entity Model
    /// </summary>
    public class NewsEntity : IEntity<NewsEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_Title { get; set; }
        public String F_Content { get; set; }
        public Int32? F_Type { get; set; }
        public DateTime? F_CreateTime { get; set; }
        public String F_Author { get; set; }
        public String F_UserID { get; set; }
        public Int32? F_Sort { get; set; }
        public Boolean? F_IsHead { get; set; }

        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

    }
}