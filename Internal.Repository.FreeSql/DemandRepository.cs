using FreeSql;
using Internal.Data;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Repository.FreeSqlServer
{ 
    public class DemandRepository : IDemandRepository
    {
        private readonly IFreeSql freeSql;

        public DemandRepository(IFreeSql freeSql)
        {
            this.freeSql = freeSql;
            var repository = freeSql.GetGuidRepository<Demand>();
        }
        public Task<int> AddAsync(Demand model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Demand model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdsAsync(object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<Demand>> QueryAsync(Expression<Func<Demand, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<Demand>> QueryAsync(Expression<Func<Demand, bool>> whereExpression, string strOrderByFileds)
        {
            throw new NotImplementedException();
        }

        public Task<Demand> QueryByIDAsync(object objId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Demand>> QueryPageAsync(Expression<Func<Demand, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Demand>> QueryPageAsync(PageParam pageParam)
        {
            Expression<Func<Demand, bool>> where = null;
            where = a => a.ClientFile.ClientName.Contains("托田") && a.ClientFile.CloseDate == null;
            var query = this.freeSql.Select<Demand>()//.Include(a=>a.Customer)
                .LeftJoin(a => a.Customer.ID == a.ClientName)
                .LeftJoin(a => a.ClientFile.ID == a.ClientName)
                .Where(where)
                .Page(pageParam.PageIndex, pageParam.PageSize);
            // var data1 = await query.ToListAsync<ViewModelDemand>();
          
            var data = await query.ToListAsync();
            return data;
        }

        public Task<List<Demand>> QueryPageExAsync(Expression<Func<Demand, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, Expression<Func<Demand, object>> orderByFiledExpression = null, bool asc = true)
        {
            throw new NotImplementedException();
        }

        public Task<Demand> SingleAsync(Expression<Func<Demand, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Demand model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Demand entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            throw new NotImplementedException();
        }
    }
}
