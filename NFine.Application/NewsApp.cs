//-----------------------------------------------------------------------
// <copyright file=" News.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: News.cs
// * history : Created by T4 01/30/2019 12:19:03 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using NFine.Domain.IRepository.News;
using NFine.Repository.News;
using System;
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.News
{
    public class NewsApp
    {
		private INewsRepository service = new NewsRepository();

		public List<NewsEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<NewsEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public List<NewsEntity> GetListByCount(int count)
        {
            var expression = ExtLinq.True<NewsEntity>();
            return service.IQueryable(expression).Take(count).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public NewsEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(NewsEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(NewsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }
    }
}