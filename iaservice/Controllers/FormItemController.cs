using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using iaservice.DataObjects;
using iaservice.Models;
using iaservice.DataObjects;

namespace iaservice.Controllers
{
    public class FormItemController : TableController<FormItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            IAserviceContext context = new IAserviceContext();
            DomainManager = new EntityDomainManager<FormItem>(context, Request);
        }

       
        // GET tables/FormItem
        public IQueryable<FormItem> GetAllFormItem()
        {
            return Query(); 
        }

        // GET tables/FormItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FormItem> GetFormItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FormItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FormItem> PatchFormItem(string id, Delta<FormItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/FormItem
        public async Task<IHttpActionResult> PostFormItem(FormItem item)
        {
            FormItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FormItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFormItem(string id)
        {
             return DeleteAsync(id);
        }
    }
}
