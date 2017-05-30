using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRNGroupApp.Data
{
    public class ShoppingList
    {
        [Key]
        public int ShoppingListId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Name { get; set; }

        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Please enter a Hex Value.")]
        public string Color { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString()
        {
            return $"[{ShoppingListId}] {Name}";
        }

        public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}

