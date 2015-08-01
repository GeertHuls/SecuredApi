using System;
using System.Collections.Generic;
using System.Web.Http;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Authorize]
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
