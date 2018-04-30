using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewLetter.Models;
using System.ComponentModel.DataAnnotations;

namespace NewLetter.Areas.Admin.Models
{
    public class Skills : BaseRepository<skill>
    {
        public Skills(IUnitOfWork unit) : base(unit)
        {
        }
    }
}