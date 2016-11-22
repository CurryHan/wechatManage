using Sensing.Data;
using Sensing.Entities.Versions;
using SensingCloud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SensingSite.APIs
{
    public class SoftwareClientApiController : Controller
    {
        private readonly ISoftwareVersionService _softwareVersionSvc;
        private readonly SensingSiteDbContext dbContext;
        public SoftwareClientApiController(ISoftwareVersionService softwareVersionSvc, SensingSiteDbContext dbContext)
        {
            _softwareVersionSvc = softwareVersionSvc;
            this.dbContext = dbContext;
        }

        [Route("api/software")]
        [HttpGet]
        public async Task<ActionResult> All()
        {
            //var details = await _softwareVersionSvc.GetAllAsync();
            var details = new SoftwareClientDetails[]
            {
                new SoftwareClientDetails { VersionNumber="1.0", Created = DateTime.Now, Creator="System", Name="Great" },
                new SoftwareClientDetails { VersionNumber="2.0", Created = DateTime.Now, Creator="System", Name="Great" }
            };
            return Json(details,JsonRequestBehavior.AllowGet);
        }

        [Route("api/software/{id:int}")]
        [HttpGet]
        public async Task<ActionResult> GetDetailsById(int id)
        {
            
            var details = _softwareVersionSvc.GetById(id);
            return new SmartJsonResult
            {
                Data = details
            };
        }

        [Route("api/software/{id:int}/latest")]
        [HttpGet]
        public async Task<ActionResult> GetUpperVersionById(int id)
        {
            var upperClients = _softwareVersionSvc.GetUpperVesionByIdAsync(id);
            return Json(upperClients);
        }

        [Route("api/software/create/{max:int}")]
        //[HttpPost]
        public async Task<ActionResult> CreatePatch(int max)
        {
            var client = new SoftwareClientDetails
            {
                Created = DateTime.UtcNow,
                Creator = "System",
                Description = "第一个版本",
                FullLink = "https:\\1.0.zip",
                Link = @"software\1.0.zip",
                VersionNumber = string.Format("{0}.0.0.0",max),
                Updater = "System",
                Name = "Dev1"
            };

            //dbContext.ClientDetails.Add(client);
            //dbContext.SaveChanges();
            _softwareVersionSvc.Add(client);
            _softwareVersionSvc.Save();
            return new ApiResult
            {
                Data = client.Id,
                Message = "Update Created successfully."
            };
        }
    }
}
