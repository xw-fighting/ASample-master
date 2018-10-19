using ASample.EntityFramework.Storage.Repository;
using ASample.Main.Domain.Models.AggregateRoots;
using System;

/// <summary>
/// 在文件夹 ASample.Main.Storage.Repository
/// </summary>
namespace ASample.Main.Storage
{

    public class UserLoginRepository:BasicEntityFrameworkRepository<ASampleMainContext,UserLogin,Guid>
    {

    }
}
