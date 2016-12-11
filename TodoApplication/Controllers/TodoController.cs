using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using TodoApplication.Models;

namespace TodoApplication.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();    
            var model = _repository.GetAll(userId);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                var userId = await GetCurrentUserIdAsync();
                var newItem = new TodoItem(item.Text, userId);
                _repository.Add(newItem);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public async Task<IActionResult> MarkAsCompleted(TodoItem item)
        {
            _repository.MarkAsCompleted(item.Id, await GetCurrentUserIdAsync());
            return RedirectToAction("Index");
        }

        public async Task<Guid> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return new Guid(user.Id);
        }

    }
}
