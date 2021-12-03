using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using DatabaseScaffoldApplication.Infrastructure;
using DatabaseScaffoldApplication.Domain.Repositories;
using DatabaseScaffoldApplication.Domain.Models;

namespace DatabaseScaffoldApplication.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {

        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResourceController(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {

            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {



            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var resource = new UserResource();


            if (resource != null)
            {
                var result = await _authorizationService.AuthorizeAsync(User, resource, Operations.Edit);

                if (result.Succeeded)
                {
                    return View(resource);
                }
                else
                {
                    return RedirectToAction("Authorize", "Account");
                }
            }

            return NotFound();
        }

    }
}