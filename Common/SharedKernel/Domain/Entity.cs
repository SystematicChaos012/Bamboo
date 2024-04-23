namespace SharedKernel.Domain
{
    /// <summary>
    /// 无主键实体
    /// </summary>
    public abstract class Entity
    {
    }

    /// <summary>
    /// 实体
    /// </summary>
    public abstract class Entity<TKey>
    {
        protected Entity()
        {
        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        /// <inheritdoc/>
        public virtual TKey Id { get; protected set; } = default!;
    }
}
