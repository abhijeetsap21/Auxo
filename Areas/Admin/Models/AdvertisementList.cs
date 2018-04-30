using NewLetter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLetter.Areas.Admin.Models
{
    public class AdvertisementList : BaseRepository<advertisementList>
    {
        public AdvertisementList(IUnitOfWork unit) : base(unit)
        {

    }
}
}