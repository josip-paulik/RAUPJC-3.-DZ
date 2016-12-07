using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoSQLRepository
{
    class TodoAccessException : Exception
    {
        public TodoAccessException(string message) : base(message)
        {
            
        }
    }
}
