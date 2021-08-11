using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    public class TestController : Controller
    {



 
       [HttpGet]
       [AllowAnonymous]
       public IActionResult Default(int id)
        {
            int ida = id;
            return View();
        }
    }
}
