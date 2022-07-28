using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parking_project_api.Models;

namespace parking_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ProjectParkingJARSContext _context;

        public ReservationsController()
        {
            _context = new ProjectParkingJARSContext();
        }

        #region Requests

        // GET: Reservations
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.Select(reservationItem => new Reservation()
            {
                Id = reservationItem.Id,
                ReservationDate = reservationItem.ReservationDate,
                TotalDue = reservationItem.TotalDue,
                ParkingSpaceId = reservationItem.ParkingSpaceId,
                CustomerId = reservationItem.CustomerId
            }).ToListAsync();
        }


        // GET: Reservation by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: Reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: Reservations
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);

            if (reservation == null)
            {

            }

            _context.Reservations.Remove(reservation);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }


        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        #endregion Requests
    }
}
