using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewLetter.Models;
using System.ComponentModel.DataAnnotations;

namespace NewLetter.Areas.Admin.Models
{
    public class Industry : BaseRepository<industry>
    {
        public Industry(IUnitOfWork unit) : base(unit)
        {
        }
    }
}