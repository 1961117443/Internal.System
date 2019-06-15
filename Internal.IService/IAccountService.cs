using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IService
{
    public interface IAccountService
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Task<int> Login(string userName, string passWord);
    }
}
