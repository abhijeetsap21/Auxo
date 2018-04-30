using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewLetter.Models;
using System.ComponentModel.DataAnnotations;

namespace NewLetter.Areas.Admin.Models
{
    public partial class Slots : BaseRepository<NewLetter.Models.slot>
    {
        public Slots(IUnitOfWork unit) : base(unit)
        {
        }

        [Required(ErrorMessage = "Kindly Enter timings in the for '1 AM - 2 AM")]
        public string slotTime { get; set; }
    }
}
    