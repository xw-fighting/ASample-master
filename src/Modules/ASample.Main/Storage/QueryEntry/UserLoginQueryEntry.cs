using ASample.EntityFramework.Storage.QueryEntry;
using ASample.Main.Domain;
using ASample.Main.Domain.Models.AggregateRoots;
using System;

/// <summary>
/// 在文件夹 ASample.Main.Storage.QueryEntry下
/// </summary>
namespace ASample.Main.Storage
{
    public class UserLoginQueryEntry:BasicEntityFrameworkQueryEntry<ASampleMainContext,UserLogin,Guid>,IUserLoginQueryEntry
    {

    }
}
