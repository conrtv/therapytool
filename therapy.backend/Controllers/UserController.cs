﻿using Microsoft.AspNetCore.Mvc;

namespace therapy.backend.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}