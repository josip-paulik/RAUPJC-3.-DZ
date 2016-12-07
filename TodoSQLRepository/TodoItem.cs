using GenericList;
using System;

namespace Models
{
    /// <summary>
    /// This class represents some kind of activity that must be done.
    /// </summary>
    public class TodoItem
    { 
        public readonly Guid Id;
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Nullable date time.
        /// DateTime is value type and won’t allow nulls. 
        /// DateTime? is nullable DateTime and will accept nulls. 
        /// Use null when todo completed date does not exist (e.g. todo is still not completed) 
        /// </summary>
        public DateTime? DateCompleted { get; set; }

        public DateTime DateCreated { get; set; }

        /// <summary>
        /// UserId that owns this TodoItem.
        /// </summary>
        public Guid UserId { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = new Guid();
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now;
            UserId = userId;
        }

        public TodoItem()
        {
            
        }

        

        public void MarkAsCompleted()
        {
            if(!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
            }
        }
    }
}
