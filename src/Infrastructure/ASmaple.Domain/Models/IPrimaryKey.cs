using System;

namespace ASmaple.Domain.Models
{
    public interface IPrimaryKey<T>
    {
        T Id { get; set; }
    }

    public interface IPrimaryKey : IPrimaryKey<Guid>
    {

    }
}
