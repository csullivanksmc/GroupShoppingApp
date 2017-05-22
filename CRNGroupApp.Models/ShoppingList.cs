using System;
using CRNGroupApp.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNGroupApp.Models
{
    public class ShoppingList
    {
        [Key]
        public int ShoppingListId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }


        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name="Changed")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString() => Name;

    }
}
