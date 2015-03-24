using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BottomsUp.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        protected readonly IBottomsRepository _repo;
        protected readonly ModelFactory _modelFactory;

        public BaseApiController(IBottomsRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }
    }
}
