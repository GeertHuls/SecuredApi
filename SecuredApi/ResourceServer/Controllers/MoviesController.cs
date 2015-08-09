using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using ResourceServer.Models;
using Thinktecture.IdentityModel.WebApi;

namespace ResourceServer.Controllers
{
    [Authorize]
    [ResourceAuthorize("Read", "Movies")]
    [EnableCors("*", "*", "GET")]
    public class MoviesController : ApiController
    {
        public IEnumerable<Item> Get()
        {
            yield return new Item
            {
                Id = Guid.NewGuid(),
                Description = "The Big Lebowski"
            };

            yield return new Item
            {
                Id = Guid.NewGuid(),
                Description = "Titanic"
            };
        } 
    }
}
