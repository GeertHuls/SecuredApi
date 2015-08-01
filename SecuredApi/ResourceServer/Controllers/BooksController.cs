using System;
using System.Collections.Generic;
using System.Web.Http;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Authorize]
    public class BooksController : ApiController
    {
        public IEnumerable<Item> Get()
        {
            yield return new Item
                {
                    Id = new Guid("cd2632f4-d48e-4d0c-bf05-2815f6d35e86"),
                    Description = "Sql performance explained"
                };

            yield return new Item
                {
                    Id = new Guid("44fb82a7-e224-4307-82f1-04761dc41853"),
                    Description = "Real-World Functional Programming"
                };
        } 
    }
}
