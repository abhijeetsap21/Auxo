using NewLetter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLetter.Areas.Admin.Models
{
    public partial class Education_ : BaseRepository<Education>
    {

        public Education_(IUnitOfWork unit) : base(unit)
        {

        }

     
    }
}