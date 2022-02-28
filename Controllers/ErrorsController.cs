using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VseVerification.Controllers;

public class ErrorsController : Controller
{
    [AllowAnonymous]
    [HttpGet("/error")]
    
    public IActionResult Error() => View("Error");
}