using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.IService
{
    public interface IFreeSqlFactory
    {
        IFreeSql Build(string constr);
    }
}
