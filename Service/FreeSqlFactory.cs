using Internal.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Service
{
    public class FreeSqlFactory : IFreeSqlFactory
    {
        private static readonly object locker = new object();
        Dictionary<string, IFreeSql> cache = new Dictionary<string, IFreeSql>();
        public IFreeSql Build(string constr)
        {
            if (!cache.ContainsKey(constr))
            {
                lock (locker)
                {
                    if (!cache.ContainsKey(constr))
                    {
                        var freesql = new FreeSql.FreeSqlBuilder()
                                         .UseConnectionString(FreeSql.DataType.SqlServer, constr)
                                         .UseMonitorCommand(cmd => System.Diagnostics.Trace.WriteLine(cmd.CommandText))
                                         .Build();
                        cache.Add(constr, freesql); 
                    }
                }
            } 
            return cache[constr]; 
        }
    }
}
