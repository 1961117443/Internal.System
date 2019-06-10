﻿using Internal.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Repository.SqlServer
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class,new()
    {
        #region 引用sqlsugar
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<TEntity> entityDB;

        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        internal SimpleClient<TEntity> EntityDB
        {
            get { return entityDB; }
            private set { entityDB = value; }
        }
        public BaseRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<TEntity>(db);
        }
        #endregion 

        #region 新增
        public async Task<int> Add(TEntity model)
        {
            return await db.Insertable(model).ExecuteCommandAsync();
        } 
        #endregion
        #region 删除

        public async Task<bool> Delete(TEntity model)
        {
            var r = await db.Deleteable(model).ExecuteCommandAsync();
            return r > 0;
        }

        public async Task<bool> DeleteById(object id)
        {
            int r = await db.Deleteable(id).ExecuteCommandAsync();
            return r > 0;
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            int r = await db.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
            return r > 0;
        } 
        #endregion

        #region 查询
        public async Task<TEntity> QueryByID(object objId)
        {
            return await db.Queryable<TEntity>().InSingleAsync(objId);
        }
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync(); 
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                .ToListAsync();
        } 
        #endregion

        public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await db.Queryable<TEntity>()
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .ToPageListAsync(intPageIndex, intPageSize);
        }

        #region 更新
        public async Task<bool> Update(TEntity model)
        {
            int r = await db.Updateable<TEntity>(model).ExecuteCommandAsync();
            return r > 0;
        }

        public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            var updater = db.Updateable<TEntity>(entity);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                updater= updater.IgnoreColumns(lstIgnoreColumns.ToArray());
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                updater = updater.UpdateColumns(lstColumns.ToArray());
            }  
            int r = await updater.ExecuteCommandAsync();
            return r > 0;
        } 
        #endregion
    }
}