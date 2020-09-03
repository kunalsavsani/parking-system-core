using System;
using System.Collections.Generic;
//using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystemCore;

namespace ParkingSystemCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class parkingslotsController : ControllerBase

    {


        private readonly dbcontext db;
        public parkingslotsController(dbcontext context)
        {
            db = context;
        }

        // GET: api/parkingslots1
        [HttpGet]
        public List<parkingslot> GetParkingslots()
        {
            return db.Parkingslots.ToList();
        }

        // GET: api/parkingslots1/5
        [ResponseType(typeof(parkingslot))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParkingslotsById(int id)
        {
            parkingslot parkingslot = await db.Parkingslots.FindAsync(id);
            if (parkingslot == null)
            {
                return NotFound("Id is not valid");
            }

            return Ok(parkingslot);
        }

        // PUT: api/parkingslots1/5
        [ResponseType(typeof(void))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Putparkingslot(int id, [FromBody] parkingslot parkingslot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parkingslot.sl)
            {
                return BadRequest("Id is not valid");
            }

            db.Entry(parkingslot).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!parkingslotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        // POST: api/parkingslots1
        [ResponseType(typeof(parkingslot))]
        [HttpPost]
        public async Task<IActionResult> Postparkingslot(parkingslot parkingslot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            parkingslot p = db.Parkingslots.Where(x => x.floor == parkingslot.floor && x.section == parkingslot.section && x.slot == parkingslot.slot).FirstOrDefault();

            if (p == null)
            {

                db.Parkingslots.Add(parkingslot);
                await db.SaveChangesAsync();
                // return Ok(parkingslot);
                return CreatedAtAction("GetParkingslotsById", new { id = parkingslot.sl }, parkingslot);
            }
            else
            {
                return BadRequest("This slot is already present in database");
            }
        }

        // DELETE: api/parkingslots1/5
        [ResponseType(typeof(parkingslot))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteparkingslot(int id)
        {
            parkingslot parkingslot = await db.Parkingslots.FindAsync(id);
            if (parkingslot == null)
            {
                return NotFound("Id is not valid");
            }

            db.Parkingslots.Remove(parkingslot);
            await db.SaveChangesAsync();

            return Ok(parkingslot);
        }



        private bool parkingslotExists(int id)
        {
            return db.Parkingslots.Count(e => e.sl == id) > 0;
        }
    }
}










    