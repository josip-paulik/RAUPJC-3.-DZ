using GenericList;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// This class represents some kind of activity that must be done.
    /// </summary>
    public class TodoItem
    { 

        public Guid Id { get; set; }

        [Required]
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
        [Required]
        public Guid UserId { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now;
            UserId = userId;
        }

        public TodoItem()
        {
            
        }

        public override bool Equals(Object obj)
        {
            if (obj is TodoItem)
            {
                var item = (TodoItem) obj;
                return item.Id.Equals(Id) && item.Id.Equals(Text);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Text.GetHashCode();
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
