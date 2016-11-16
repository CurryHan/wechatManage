using Sensing.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SensingCloud.Controllers
{
    [Authorize]
    public class SoftwareController : LanguageController
    {
        private readonly SensingSiteDbContext _dataContext;

        public SoftwareController(SensingSiteDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
