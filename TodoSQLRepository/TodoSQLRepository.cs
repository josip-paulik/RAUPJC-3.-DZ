using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericList;
using Interfaces;
using Models;

namespace TodoSQLRepository
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly IGenericList<TodoItem> _list;
        private readonly TodoDbContext _context;

        public TodoItem Get(Guid todoId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Add(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            throw new NotImplementedException();
        }

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }
    }
}
