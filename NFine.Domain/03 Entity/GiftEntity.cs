//-----------------------------------------------------------------------
// <copyright file=" Gift.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Gift.cs
// * history : Created by T4 04/21/2019 10:43:29 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.Gift
{
    /// <summary>
    /// Gift Entity Model
    /// </summary>
    public class GiftEntity : IEntity<GiftEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_GiftName { get; set; }
        public Int32? F_VCoin { get; set; }
        public String F_Gif_Url { get; set; }
        public String F_Png_Url { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public String F_LastModifyUserId { get; set; }
    }
}