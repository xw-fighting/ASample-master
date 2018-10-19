using ASample.EntityFramework.Domain.Repository;
using ASample.Main.Domain.Models.AggregateRoots;
using System;

/// <summary>
/// 在文件夹 ASample.Main.Domain.Repository
/// </summary>
namespace ASample.Main.Domain
{
    public interface IUserLoginRepository : IBasicEntityFrameworkRepository<UserLogin,Guid>
    {

    }
}
