using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TodoApplication.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var userId = Guid.NewGuid();
            TodoItem item = new TodoItem("NOW, GO", userId);
            _repository.Add(item);
            var model = _repository.GetAll(userId);
            return View(model);
        }

    }
}
