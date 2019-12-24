using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Util.Enum
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        SuperAdministrator = 0,
        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 1,
        /// <summary>
        /// 一般用户
        /// </summary>
        GeneralUser = 2
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 未指定
        /// </summary>
        All = -1,
        /// <summary>
        /// 已禁用
        /// </summary>
        Forbidden = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1
    }
}
