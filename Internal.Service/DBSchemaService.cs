using Internal.Common.Helpers;
using Internal.IRepository;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Internal.Service
{
    public class DBSchemaService : IDBSchemaService
    {
        const string EnterLine = "\r\n";
        /// <summary>
        /// 
        /// </summary>
        private readonly IDBSchema schema;

        public DBSchemaService(IDBSchema schema)
        {
            this.schema = schema;
        }

        protected string SummaryLine(string summary)
        {
            return $"/// <summary>\r\n/// {summary} \r\n/// <summary>";
        }
        public async Task<string> DbToCSharp(string tableName)
        {
            var dataSet =await this.schema.GetSqlSchema(tableName);
            var table = dataSet.Tables[tableName];
            var tableColumn = dataSet.Tables["TableColumn"];

            Dictionary<string, bool> columns = new Dictionary<string, bool>();
            foreach (var row in tableColumn.AsEnumerable())
            {
                columns.Add(row.Field<string>("name"), row.Field<bool>("is_nullable"));
            } 
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(SummaryLine(tableName)); 
            stringBuilder.AppendLine($"public partial class {tableName}");
            stringBuilder.AppendLine("{");
            foreach (DataColumn col in table.Columns)
            {
                string columnName = col.ColumnName;
                string nullable = "";
                if (columns[columnName] && col.DataType.Nullable())
                {
                    nullable = "?";
                } 
                stringBuilder.AppendTabLine($"\t/// <summary>");
                stringBuilder.AppendTabLine($"\t/// {columnName}");
                stringBuilder.AppendTabLine($"\t/// <summary>");
                stringBuilder.AppendTabLine($"\tpublic {col.DataType.ShortName()}{nullable} {columnName} {{ get; set; }}");
                
            } 
            stringBuilder.AppendLine("}"); 
            return stringBuilder.ToString();

        }

         
     
    }
}
