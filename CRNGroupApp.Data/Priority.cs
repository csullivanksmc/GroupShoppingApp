using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNGroupApp.Data

{
    public enum Priority
    {
        [Display(Name = "It Can Wait.")]
        Low = 0,
        [Display(Name = "Need It Soon.")]
        Moderate = 1,
        [Display(Name = "Get It Now!")]
        High = 2

    }
}
