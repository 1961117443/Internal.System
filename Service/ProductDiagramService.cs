using Internal.Data;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Service
{
    public class ProductDiagramService : IBaseService<ProductDiagram>
    {
        private readonly IFreeSql freeSql;

        public ProductDiagramService(IFreeSqlFactory freeSqlFactory)
        {
            this.freeSql = freeSqlFactory.Build("server=39.108.89.110;uid=sa;pwd=sa123.;database=FileData");
        }
         

        public Task<int> AddAsync(ProductDiagram model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ProductDiagram model)
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

        public Task<List<ProductDiagram>> QueryAsync(Expression<Func<ProductDiagram, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDiagram>> QueryAsync(Expression<Func<ProductDiagram, bool>> whereExpression, string strOrderByFileds)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDiagram> QueryByIDAsync(object objId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDiagram>> QueryPageAsync(int intPageIndex, int intPageSize, string strOrderByFileds = null)
        {
            var list =await freeSql.Select<ProductDiagram>().Page(intPageIndex, intPageSize).ToListAsync();
            return list;
        }

        public Task<List<ProductDiagram>> QueryPageAsync(Expression<Func<ProductDiagram, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDiagram>> QueryPageExAsync(Expression<Func<ProductDiagram, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, Expression<Func<ProductDiagram, object>> orderByFiledExpression = null, bool asc = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ProductDiagram model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ProductDiagram entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            throw new NotImplementedException();
        }
    }
}
