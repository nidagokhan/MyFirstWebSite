﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Controllers
{
    public class ContactController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
