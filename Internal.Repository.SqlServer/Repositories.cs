using Internal.Data.Entity;
using Internal.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Repository.SqlServer
{
    /// <summary>
    /// 评论模块数据访问层
    /// </summary>
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
    }


    public class SysPermissionRepository : BaseRepository<SysPermission>, ISysPermissionRepository
    {
    }
    public class SysUserRoleRepository : BaseRepository<SysUserRole>, ISysUserRoleRepository
    {
    }
    public class SysRolePermissionRepository : BaseRepository<SysRolePermission>, ISysRolePermissionRepository
    {
    }
}
