﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DefaultController : ApiController
    {
        private db db = new db();

        [Route("getlist")]
        public List<WpfApp1.Employee> Get() => db.GetList();

        [Route("getlist/{ID}")]
        public WpfApp1.Employee Get(int id) { return db.GetById(id); }

        [Route("addemployee")]
        public HttpResponseMessage Post([FromBody]WpfApp1.Employee value)
        {
            if (db.AddEmployee(value))
            {
                
                return Request.CreateResponse(HttpStatusCode.Created);
            }
                
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
