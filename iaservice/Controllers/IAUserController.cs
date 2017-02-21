using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Linq;
using iaservice.DataObjects;
using iaservice.Models;
using System.Web.Http.Controllers;
using iaservice.Extensions;
using System.Net.Http;

namespace iaservice.Controllers
{
    [MobileAppController]
    [RoutePrefix("api/v1.0")]
    public class IAUserController : ApiController
    {
        IAserviceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            context = new IAserviceContext();
        }

        [HttpGet, Route("getUser")]
        //We are sending email only 
        public async Task<string> Get(string mobileServiceID)
        {
            var info = await GetUserInfo();

            if (info != null)
            {
                //Create a new EntityData instance with the info from Azure AD & the mobileServiceID
                var userObj = new User(info, mobileServiceID);

                //1. Get User from DB in connected context in case we need to update instead
                var originaluser = context.Users.Find(userObj.mobileserviceID);

                //2. Check to see if we found an existing entry, otherwise skip to 5
                if (originaluser != null)
                {
                    //3. Update our new EntityData instance with original record
                    userObj.UpdateEntityData(originaluser);

                    //4. Set the values on the original entity
                    context.Entry(originaluser).CurrentValues.SetValues(userObj);
                }
                else
                {
                    //5. Add the new user to the database
                    context.Users.Add(userObj);
                }

                //6. Save database context
                await context.SaveChangesAsync();

                return "SUCCESS";//CreatedAtRoute("getUserInfo",new { id = userObj.Id },userObj);
            }

            return "FALED";//NotFound();
        }

        // GET USER'S EMAIL ADDRESS FROM AZURE AD
        private async Task<UserInfo> GetUserInfo()
        {
            string provider = ((ClaimsPrincipal)User).FindFirst("http://schemas.microsoft.com/identity/claims/identityprovider").Value;
            UserInfo _info = new UserInfo();
            if (provider.Equals("aad"))
            {
                var credentials = await User.GetAppServiceIdentityAsync<AzureActiveDirectoryCredentials>(Request);
                var _email = credentials.UserClaims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value ?? string.Empty;
                _info.email = credentials.UserId;
                _info.firstname = credentials.UserClaims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
                _info.lastname = credentials.UserClaims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value ?? string.Empty;
            }
            return _info;
        }

    }
}
