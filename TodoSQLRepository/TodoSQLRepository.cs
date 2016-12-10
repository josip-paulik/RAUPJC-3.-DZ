using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericList;
using Interfaces;
using Models;
using TodoSQLRepository.Exception_classes;

namespace TodoSQLRepository
{
    public class TodoSqlRepository : ITodoRepository
    {
        
        private readonly TodoDbContext _context;

        public TodoItem Get(Guid todoId, Guid userId)
        {
            //using (_context)
            {
                var todoItem = _context.TodoItems.FirstOrDefault(i => i.Id.Equals(todoId));

                if (!todoItem.Equals(null) && !userId.Equals(todoItem.Id))
                {
                    throw new TodoAccessException("This user cannot access this item.");
                }

                return todoItem;
            }

            
        }

        public void Add(TodoItem todoItem)
        {
            //using (_context)
            {
                if (_context.TodoItems.Select(i => i.Id).Contains(todoItem.Id))
                {
                    throw new DuplicateTodoItemException("This item already exists.");
                }

                _context.TodoItems.Add(todoItem);
                _context.SaveChanges(); 
            }

        }

        public bool Remove(Guid todoId, Guid userId)
        {

            //using (_context)
            {
                if (!todoId.Equals(userId))
                {
                    throw new TodoAccessException("This user cannot access this item.");
                }

                var todoItem = _context.TodoItems.FirstOrDefault(i => i.Id.Equals(todoId));

                if (todoItem == null)
                {
                    return false;
                }

                _context.TodoItems.Remove(todoItem);
                _context.SaveChanges();
            }

            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            //using (_context)
            {
                if (!todoItem.Id.Equals(userId))
                {
                    throw new TodoAccessException("User does not have access to this item.");
                }


                _context.TodoItems.AddOrUpdate(todoItem);
                _context.SaveChanges(); 
            }
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            //using (_context)
            {
                if (!todoId.Equals(userId))
                {
                    throw new TodoAccessException("User does not have access to this item.");
                }

                var item = _context.TodoItems.FirstOrDefault(i => i.Id.Equals(todoId));

                if (item != null)
                {
                    item.MarkAsCompleted();
                    Update(item, userId);
                    return true;
                }

                return false; 
            }
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            //using (_context)
            {
                return _context.TodoItems.Where(i => i.UserId.Equals(userId))
                                .OrderByDescending(i => i.DateCreated)
                                .ToList(); 
            }
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            //using (_context)
            {
                return _context.TodoItems.Where(i => i.UserId.Equals(userId) && !i.IsCompleted)
                                .OrderByDescending(i => i.DateCreated)
                                .ToList(); 
            }
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            //using (_context)
            {
                return _context.TodoItems.Where(i => i.UserId.Equals(userId) && i.IsCompleted)
                               .OrderByDescending(i => i.DateCreated)
                               .ToList(); 
            }
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            //using (_context)
            {
                return _context.TodoItems.Where(filterFunction).Where(i => i.UserId.Equals(userId))
                               .OrderByDescending(i => i.DateCreated)
                               .ToList(); 
            }
        }

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }
    }
}
