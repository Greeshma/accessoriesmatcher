using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using AccessoriesMatcherService.DataObjects;
using AccessoriesMatcherService.Models;
using System.Collections.Generic;
using System.Web.Http.Tracing;

namespace AccessoriesMatcherService.Controllers
{
    public class DressesController : TableController<Dress>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            AccessoriesMatcherContext context = new AccessoriesMatcherContext();
            DomainManager = new EntityDomainManager<Dress>(context, Request);
        }

        // GET tables/dresses
        public IQueryable<Dress> GetAllDresses()
        {
            return Query();
        }

        // GET tables/dresses/<id>
        public SingleResult<Dress> GetDress(string id)
        {
            var item = Lookup(id);
            var items = item.Queryable;

            var dress = items.First();



            return new SingleResult<Dress>((new List<Dress>() { dress }).AsQueryable());

            //return Lookup(id);
        }

        // PATCH tables/dresses/<id>
        public Task<Dress> PatchDress(string id, Delta<Dress> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/dresses/<id>
        public async Task<IHttpActionResult> PostDress(Dress item)
        {
            Dress current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { Id = current.Id }, current);
        }

        //// PUT tables/dresses
        //public async Task<IHttpActionResult> PutDressAsync()
        //{
        //    Dress item = new Dress()
        //    {
        //        Id = "3",
        //        userid = 200,
        //        colour = "Green",
        //        //CreatedAt = new DateTimeOffset(DateTime.UtcNow),
        //        //Deleted = false,
        //        //UpdatedAt = new DateTimeOffset(DateTime.UtcNow),
        //        //Version = new byte[] { 0x32 }
        //    };

        //    Dress current = await InsertAsync(item);

        //    return CreatedAtRoute("Tables", new { Id = current.Id }, current);
        //}


        // DELETE tables/dresses/<id>
        public Task DeleteDress(string id)
        {
            return DeleteAsync(id);
        }
    }
}