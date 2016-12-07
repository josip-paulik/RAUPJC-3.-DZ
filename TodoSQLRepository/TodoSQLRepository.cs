using System;
using System.Collections.Generic;
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
        private readonly IGenericList<TodoItem> _list;
        private readonly TodoDbContext _context;

        public TodoItem Get(Guid todoId, Guid userId)
        {
            var todoItem = _list.FirstOrDefault(i => i.Id == todoId);

            if (!todoItem.Equals(null) && !userId.Equals(todoItem.Id))
            {
                throw new TodoAccessException("This user cannot access this item.");
            }

            return todoItem;
        }

        public void Add(TodoItem todoItem)
        {
            if (!_list.FirstOrDefault(i => i.Id.Equals(todoItem.Id)).Equals(null))
            {
                throw new DuplicateTodoItemException("duplicate id: {id}");
            }
            
            _list.Add(todoItem);
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            var todoItem = _list.FirstOrDefault(i => i.Id.Equals(todoId));

            if (!todoItem.Id.Equals(userId))
            {
                throw new TodoAccessException("This user cannot access this item.");
            }

            return _list.Remove(todoItem);
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            foreach (var item in _list)
            {
                if (item.Id == todoItem.Id)
                {
                    if (!item.UserId.Equals(userId))
                    {
                        throw new TodoAccessException("This user cannot access this item.");
                    }
                    item.Text = todoItem.Text;
                }
            }
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            foreach (var item in _list)
            {
                if (item.Id == todoId)
                {
                    if (!item.UserId.Equals(userId))
                    {
                        throw new TodoAccessException("This user cannot access this item.");
                    }

                    item.MarkAsCompleted();
                    return true;
                }
            }

            return false;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _list.Where(i => i.UserId.Equals(userId))
                        .OrderByDescending(i => i.DateCreated)
                        .ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _list.Where(i => i.UserId.Equals(userId) && !i.IsCompleted)
                        .OrderByDescending(i => i.DateCreated)
                        .ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _list.Where(i => i.UserId.Equals(userId) && i.IsCompleted)
                        .OrderByDescending(i => i.DateCreated)
                        .ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _list.Where(filterFunction).Where(i => i.UserId.Equals(userId))
                       .OrderByDescending(i => i.DateCreated)
                       .ToList();
        }

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }
    }
}
