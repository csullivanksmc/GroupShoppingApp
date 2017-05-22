using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNGroupApp.Data
{
    public enum Priority
    {
        [Description("Grab it Now!")]
        High,
        [Description("Need it soon.")]
        Medeum,
        [Description("It can wait.")]
        Low
    }
    public class ShoppingListItem
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public int ShoppingListId { get; set; }
        [Required]
        public string Contents { get; set; }
        public bool IsChecked { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

        public Priority Priority { get; set; }
    }
}
