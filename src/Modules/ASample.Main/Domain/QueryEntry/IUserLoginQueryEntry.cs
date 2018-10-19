using ASample.EntityFramework.Domain.QueryEntry;
using ASample.Main.Domain.Models.AggregateRoots;
using System;

/// <summary>
/// 在文件夹 ASample.Main.Domain.QueryEntry下
/// </summary>
namespace ASample.Main.Domain
{
    public interface IUserLoginQueryEntry : IBasicEntityFrameworkQueryEntry<UserLogin,Guid>
    {

    }
}
