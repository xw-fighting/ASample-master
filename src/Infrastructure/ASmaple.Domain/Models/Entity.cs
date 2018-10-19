using System;

namespace ASmaple.Domain.Models
{
    public abstract class Entity : Entity<Guid>,IPrimaryKey//--->为什么要继承IPrimaryKey
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }

    public abstract class Entity<TKey> : IPrimaryKey<TKey>
    {
        public TKey Id { get; set ; }
    }
}
