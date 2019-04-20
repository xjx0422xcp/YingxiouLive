//-----------------------------------------------------------------------
// <copyright file=" News.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: News.cs
// * history : Created by T4 01/30/2019 12:19:04 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.News
{
    public class NewsMap : EntityTypeConfiguration<NewsEntity>
    {
        public NewsMap()
        {
            this.ToTable("Sys_News");
            this.HasKey(t => t.F_Id);
        }
    }
}