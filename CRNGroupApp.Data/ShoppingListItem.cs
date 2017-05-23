using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRNGroupApp.Data
{
    public class ShoppingListItem
    {
        [Key]
        public int ShoppingListItemId { get; set; }

        [ForeignKey("ShoppingList")]
        public int ShoppingListId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Content { get; set; }

        public Priority Priority { get; set; }

        [MinLength(2)]
        [MaxLength(25)]
        public string Note { get; set; }

        [Display(Name = "Purchased")]
        public bool IsChecked { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString()
        {
            return $"[{ShoppingListItemId}]";
        }

        public virtual ShoppingList ShoppingList { get; set; }
    }
}
