using NewLetter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLetter.Areas.Admin.Models
{
    public class JobStatu : BaseRepository<jobStatu>
    {
        public JobStatu(IUnitOfWork unit) : base(unit)
        {
        }
    }
}