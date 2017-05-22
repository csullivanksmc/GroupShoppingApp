using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNGroupApp.Models
{
    public class ShoppingListCreate
    {
        [Key]
        public int ShoppingListId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage ="Please enter at least 2 characters.")]
        [MaxLength(120)]
        public string Name { get; set; }

        public string Color { get; set; }

        public string ShoppingListItem { get; set; }

        public override string ToString() => Name;

    }
}
