using System;

namespace ASmaple.Domain.Models
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime? DeleteTime { get; set; }
    }
}
