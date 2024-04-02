namespace SharedKernel.Domain.Audit
{
    public static class AuditConsts
    {
        /// <summary>
        /// 创建时间 <see cref="DateTime"/>
        /// </summary>
        public const string CREATION_TIME_NAME = "CreationTime";

        /// <summary>
        /// 修改时间 <see cref="DateTime"/>
        /// </summary>
        public const string MODIFICATION_TIME_NAME = "ModificationTime";

        /// <summary>
        /// 软删除 <see cref="bool"/>
        /// </summary>
        public const string DELETED_FLAG_NAME = "Deleted";
    }
}
