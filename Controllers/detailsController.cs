using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ParkingSystemCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class detailsController : ControllerBase
    {


        private readonly dbcontext db;

        public detailsController(dbcontext context)
        {
            db = context;
        }

        [HttpGet]
        // [Authorize]
        public List<details> GetDetails()
        {
            return db.Details.ToList();
        }

        // GET: api/details/5
        [ResponseType(typeof(details))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Getdetails(string id)
        {
            details details = await db.Details.FindAsync(id);                                              //get details of the person parking based on vehicle number
            if (details == null)
            {
                return NotFound("Id is not valid");
            }

            return Ok(details);
        }

        // PUT: api/details/5

        [HttpPut("{id}")]
        //    [ResponseType(typeof(void))]

        public async Task<IActionResult> Putdetails(string id, [FromBody] details details)
        {
            //  details details = await db.Details.FindAsync(id);


            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);                                                         //update details
            }

            if (id != details.vehicleNo)
            {
                return BadRequest("Id is not valid");
            }

            if (details.outTime < details.inTime)
            {
                return BadRequest("out time cannot be smaller than in time");
            }

            string v;
            DateTime date;
            double outTime;

            if (details.outTime != null)
            {
                details.cost = (details.outTime - details.inTime) * 3;
                parkingslot p = db.Parkingslots.Where(x => x.sl == details.Slot).FirstOrDefault();
                p.availability = Availability.Available;
                backup b = new backup();

                b.name = details.name;
                b.contactNumber = details.contactNumber;
                b.vehicleNo = details.vehicleNo;
                b.date = details.date;
                // b.floor = details.serialno.floor;
                // b.slot = details.serialno.slot;
                // b.section = details.serialno.section;
                b.slot = Convert.ToInt32(details.Slot);
                b.inTime = details.inTime;
                b.outTime = Convert.ToDouble(details.outTime);
                b.cost = Convert.ToDouble(details.cost);
                db.Backups.Add(b);
                v = b.vehicleNo;
                date = b.date.Date;
                outTime = b.outTime;

                db.Entry(details).State = EntityState.Modified;




                try
                {
                    await db.SaveChangesAsync();
                    db.Details.Remove(details);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!detailsExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return CreatedAtAction("GetBackups", new { controller = "backups", id = v }, details);

            }
            else
            {
                db.Entry(details).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!detailsExists(id))
                    {
                        return NotFound("Id is not valid");
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("Getdetails", new { controller = "details", id = id }, details);
            }
        }

        [HttpPost]

        // POST: api/details
        [ResponseType(typeof(details))]
        public async Task<IActionResult> Postdetails(details details)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            parkingslot p1 = db.Parkingslots.Where(x => x.availability == Availability.Available).FirstOrDefault();

            if (p1 == null)
            {
                return BadRequest("There is no available slot to park the vehicle");
            }
            else
            {
                details.date = details.date;
                p1.availability = Availability.Occupied;
                details.Slot = p1.sl;

                db.Details.Add(details);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (detailsExists(details.vehicleNo))
                    {
                        return Conflict("This vehicle is already parked");
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction("Getdetails", new { id = details.vehicleNo }, details);
            }

        }
        [HttpDelete("{id}")]
        // DELETE: api/details/5
        [ResponseType(typeof(details))]
        public async Task<IActionResult> Deletedetails(string id)
        {
            details details = await db.Details.FindAsync(id);
            if (details == null)
            {
                return NotFound("Invalid id");
            }

            db.Details.Remove(details);
            await db.SaveChangesAsync();

            return Ok(details);
        }


        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        private bool detailsExists(string id)
        {
            return db.Details.Count(e => e.vehicleNo == id) > 0;
        }
    }
}