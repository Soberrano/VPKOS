using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using WebApplication2.Controllers.Abstract;

namespace WebApplication2.Controllers.Api.Reservation
{
    [RoutePrefix ("api/dog")]
    public class DogController : BaseApiController
    {
        protected DogManager DogManager
        {
            get
            {
                return Request.GetOwinContext().Get<DogManager>();
            }
        }

        [HttpPost]
        [Route("GetDogs")]
        public async Task<IHttpActionResult> GetDogs()
        {
            List<Dog> Dogs = await DogManager.GetDogs();
            return WrapSuccess(Dogs);
        }
       
        [HttpPost]
        [Route("AddDog")]
        public async Task AddDog()
        {
            await DogManager.AddDog();
        }

    }
}
