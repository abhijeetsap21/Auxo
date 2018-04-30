using NewLetter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLetter.Areas.Admin.Models
{    
        public class CompanyMaster : BaseRepository<companyDetail>
        {
            public CompanyMaster(IUnitOfWork unit) : base(unit)
            {

            }
        }    
}