using System;

namespace ASmaple.Domain.Models
{
    public abstract  class AggregateRoot: AggregateRoot<Guid>, ISoftDelete, IPrimaryKey
    {
        /// <summary>
        /// 赋值
        /// </summary>
        public AggregateRoot()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }
    }
    public abstract class AggregateRoot<TKey> : ISoftDelete, IPrimaryKey<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual TKey Id { get; set ; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeleteTime { get ; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifyTime { get; set; }
    }
}
