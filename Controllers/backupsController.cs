using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingSystemCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class backupsController : ControllerBase
    {

        private readonly dbcontext db;

        public backupsController(dbcontext context)
        {
            db = context;
        }

        [HttpGet]

        public List<backup> GetBackups()
        {
            return db.Backups.ToList();
        }

         


    }
}