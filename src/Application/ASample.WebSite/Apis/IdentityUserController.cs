using ASample.Identity.Domain.QueryEntry;
using ASample.Main.Domain;
using ASample.Main.Storage;
using ASample.WebSite.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Unity.Attributes;

namespace ASample.WebSite.Apis
{
    //[RoutePrefix("api/UserLogin")]
    //public class IdentityUserController : ApiController
    //{
    //    [Dependency]
    //    public IIdentityUserQueryEntry IdentityUserQueryEntry { get; set; }
    //    //public IdentityUserController(IIdentityUserQueryEntry identityUserQueryEntry)
    //    //{
    //    //    IdentityUserQueryEntry = identityUserQueryEntry;
    //    //}
    //    //public IIdentityUserQueryEntry IdentityUserQueryEntry { get; set; }

    //    [HttpPost]
    //    [Route("GetList")]
    //    public async Task<UserLoginPagedViewModel> GetList()
    //    {
    //        var result = await IdentityUserQueryEntry.SelectAsync();
    //        var list = result.Select(c => new UserLoginViewModel
    //        {
    //            Name = c.UserName,
    //            Password = c.PasswordHash,
    //            CreateTime = c.CreateTime,
    //            DeleteTime = c.DeleteTime
    //        }).ToList();
    //        return new UserLoginPagedViewModel { Rows = list, Total = list.Count };
    //        //return new UserLoginPagedViewModel { };
    //    }

    //    // GET: api/UserLogin/5
    //    public string Get(int id)
    //    {
    //        return "value";
    //    }

    //    // POST: api/UserLogin
    //    [HttpPost]
    //    [Route("api/UserLogin")]
    //    public async Task Post([FromBody]string value)
    //    {

    //    }

    //    // PUT: api/UserLogin/5
    //    public void Put(int id, [FromBody]string value)
    //    {
    //    }

    //    // DELETE: api/UserLogin/5
    //    public void Delete(int id)
    //    {
    //    }
    //}
}
