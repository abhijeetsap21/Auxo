using NewLetter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLetter.Areas.Admin.Models
{
    public partial class EducationTypes : BaseRepository<educationType>
    {
        public EducationTypes(IUnitOfWork unit):base(unit)
        {

        }        
    }
}