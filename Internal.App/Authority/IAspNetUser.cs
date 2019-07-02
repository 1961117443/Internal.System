using Internal.Common.Helpers;
using Internal.Data.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internal.App.Authority
{

    /// <summary>
    /// 当前用户信息
    /// </summary>
    public interface IAspNetUser
    {
        /// <summary>
        /// 用户id
        /// </summary>
        string Id { get;}
        /// <summary>
        /// 用户名
        /// </summary>
        string Name { get;} 
        /// <summary>
        /// 是否登陆
        /// </summary>
        bool IsLogin { get; }
        /// <summary>
        /// 用户凭据
        /// </summary>
        string AuthToken { get; }
    }
}
