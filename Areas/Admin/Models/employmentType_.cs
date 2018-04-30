using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewLetter.Models;
namespace NewLetter.Areas.Admin.Models
{
    public partial class employmentType_ : BaseRepository<EmploymentType>
    {

        public employmentType_(IUnitOfWork unit) : base(unit)
        {
        }
        
    }
}