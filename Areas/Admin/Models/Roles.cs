using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewLetter.Models;
using System.ComponentModel.DataAnnotations;

namespace NewLetter.Areas.Admin.Models
{
    public partial class Roles : BaseRepository<NewLetter.Models.role>
    {
        public Roles(IUnitOfWork unit) : base(unit)
        {
        }
    }
    
}