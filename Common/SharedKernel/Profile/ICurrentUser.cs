﻿using System.Security.Claims;

namespace SharedKernel.Profiles
{
    /// <summary>
    /// 当前用户 (基于声明的，所以取出的数据是 string 类型的)
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Id
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 查找声明
        /// </summary>
        Claim? FindClaim(string claimType);
    }
}