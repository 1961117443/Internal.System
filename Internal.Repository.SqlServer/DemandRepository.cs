using Internal.Data.Entity;
using Internal.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Internal.Data;
using System.Threading.Tasks;
using Internal.Data.ViewModel;

namespace Internal.Repository.SqlServer
{
    /// <summary>
    /// 需求录入访问层
    /// </summary>
    public class DemandRepository :BaseRepository<Demand>, IDemandRepository
    {
        protected override ISugarQueryable<Demand> GetSelect()
        {
            var q = Db.Queryable<Demand>()
                .Mapper(e => e.Customer, e => e.ClientName)
                .Mapper(e => e.ClientFile, e => e.ClientName); 
            return q;
        }

        public List<TValue> QueryPageAsync<TValue>(Func<object, TValue> func)
        {
            List<TValue> list = new List<TValue>();

            return list;
        }

        public override Task<List<Demand>> QueryPageAsync(PageParam pageParam)
        {
            List<IConditionalModel> conModels = new List<IConditionalModel>();
            foreach (var item in pageParam.Params)
            {
                conModels.Add(new ConditionalModel() { FieldName = item.Field, ConditionalType = Enum.Parse<ConditionalType>(item.Logic.ToString()), FieldValue = item.Value });
            }

            var query= Db.Queryable<Demand, ClientFile>((t1, t2) => new object[] { JoinType.Left, t1.ClientName == t2.ID });
            var model = query
                .Select((t1,t2) => new ViewModelDemand() { BillCode = t1.BillCode, ClientName =t1.ClientName, ClientFileName = t2.ClientName, ID = t1.ID , Maker = t1.Maker })
                .MergeTable()
                .Where(conModels);
            var data=model.ToPageList(pageParam.PageIndex, pageParam.PageSize);
            var t = query.MergeTable();
            return base.QueryPageAsync(pageParam);
        }

        //protected override ISugarQueryable<Demand> GetQueryable()
        //{
        //    var q= base.GetQueryable();

        //    //第一种方法 base.GetQueryable().Mapper(e=>e.Customer,e=>e.CustomerID);
        //    //SELECT * FROM (SELECT [ID],[BillCode],[CustomerID],[RecordDate],[Presenter],[Maker],[MakeDate],ROW_NUMBER() OVER( ORDER BY GetDate() ) AS RowIndex  FROM [Demand] ) T WHERE RowIndex BETWEEN 1 AND 20
        //    //exec sp_executesql N'SELECT [ID],[Code],[Name] FROM [Customer]  WHERE   ID IN (''00000000-0000-0000-0000-000000000000'',''d33eb412-ea88-4b4b-b13b-b93604d5c5f0'',''5f7a445c-de9f-41ed-9544-2da27a01b12d'',''4b3edc81-bd48-4cf6-994a-39717f3dd77d'',''360680c9-894f-4f2c-8b8b-3deb67690fbe'',''1546f810-857a-4612-836f-e9cb6dd31b2f'',''514370bd-9491-4d73-8fe3-e2e2a95dc0ce'',''b39d64ef-4ca1-402c-acff-48a0a4649b1e'',''6ce284c5-5d9b-4451-bb4b-ef2a28757706'',''5e948483-f5f6-4f31-b696-55efcf02f6a7'',''2225c75b-1fff-435e-b113-1448640c057d'',''20cf3b4c-fb07-4ae8-9088-e42fbb0224dc'')   ',N'@ConditionalID0 nvarchar(4000)',@ConditionalID0=N'00000000-0000-0000-0000-000000000000,d33eb412-ea88-4b4b-b13b-b93604d5c5f0,5f7a445c-de9f-41ed-9544-2da27a01b12d,4b3edc81-bd48-4cf6-994a-39717f3dd77d,360680c9-894f-4f2c-8b8b-3deb67690fbe,1546f810-857a-4612-836f-e9cb6dd31b2f,514370bd-9491-4d73-8fe3-e2e2a95dc0ce,b39d64ef-4ca1-402c-acff-48a0a4649b1e,6ce284c5-5d9b-4451-bb4b-ef2a28757706,5e948483-f5f6-4f31-b696-55efcf02f6a7,2225c75b-1fff-435e-b113-1448640c057d,20cf3b4c-fb07-4ae8-9088-e42fbb0224dc'
        //    Expression<Func<Demand, Customer>> mapperObject = e => e.Customer;
        //    Expression<Func<Demand, object>> mapperField = e => e.CustomerID;
        //    q = base.GetQueryable().Mapper(mapperObject, mapperField);

        //    //q = base.GetQueryable().Mapper((e, cache) =>
        //    //{
        //    //    var list = cache.GetListByPrimaryKeys<Customer>(w => w.CustomerID);
        //    //    e.Customer = list.FirstOrDefault(i => i.ID == e.CustomerID);
        //    //});
        //    return q;
        //}
    }
}
