﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace KKTraders.Controllers
{
    [AllowAnonymous]
    public class ViewOrder : Controller
    {
      
        public IActionResult MyOrder()
        {
            
            return View();
        }
    }
}
