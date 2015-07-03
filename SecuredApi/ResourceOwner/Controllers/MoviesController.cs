using System;
using System.Collections.Generic;
using System.Web.Http;
using ResourceOwner.Models;

namespace ResourceOwner.Controllers
{
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
