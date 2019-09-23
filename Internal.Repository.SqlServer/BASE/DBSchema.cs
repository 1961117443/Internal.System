using Internal.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Repository.SqlServer
{
    public partial class DBSchema: IDBSchema
    {
        #region 引用sqlsugar
        private DbContext context;
        private SqlSugarClient db; 

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
        public DBSchema()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db; 
        }
        #endregion
        public async Task<DataSet> GetSqlSchema(string tableName)
        {
            DataSet dataSet = new DataSet();
              dataSet = await Db.Ado.GetDataSetAllAsync($"SELECT * FROM [{tableName}] WHERE 1=2;" +
                $"SELECT c.name,c.is_nullable FROM sys.[columns] AS c WHERE c.[object_id]=OBJECT_ID('{tableName}')");
            /*var tableColumn = await Db.Ado.GetDataTableAsync($"SELECT c.name,c.is_nullable FROM sys.[columns] AS c WHERE c.[object_id]=OBJECT_ID('{tableName}')");*/
            dataSet.Tables[0].TableName = tableName;
            dataSet.Tables[1].TableName = "TableColumn"; 
            return dataSet;
        }
    }
}
