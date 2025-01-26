using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {

        
        [HttpGet]
        public ActionResult Listar(){
            return Ok();
        }
        
    }
}