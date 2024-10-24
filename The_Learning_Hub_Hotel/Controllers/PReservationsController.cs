using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using The_Learning_Hub_Hotel.Models;

namespace The_Learning_Hub_Hotel.Controllers
{
    public class PReservationsController : Controller
    {
        private readonly ModelContext _context;

        public PReservationsController(ModelContext context)
        {
            _context = context;
        }

        // GET: PReservations
        public async Task<IActionResult> Index()
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            var modelContext = _context.PReservations.Include(p => p.Room).Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> DownloadInvoice(decimal? id)
        {
            if (id == null || _context.PReservations == null)
            {
                return NotFound();
            }

            var pReservation = await _context.PReservations
                .FirstOrDefaultAsync(m => m.Reservationsid == id);

            if (pReservation == null || string.IsNullOrEmpty(pReservation.Invoicepdf))
            {
                return NotFound("Invoice not found.");
            }

            // Ensure Invoicepdf contains only the file name
            var fileName = Path.GetFileName(pReservation.Invoicepdf);

            // Construct the path to the invoice file
            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "invoices");
            var filePath = Path.Combine(baseDirectory, fileName);

            // Debugging output to ensure correct path
            Console.WriteLine($"Base Directory: {baseDirectory}");
            Console.WriteLine($"Full File Path: {filePath}");

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"Invoice file not found at {filePath}.");
            }

            // Read the file and return it as a downloadable file
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", fileName);
        }


        // GET: PReservations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            if (id == null || _context.PReservations == null)
            {
                return NotFound();
            }

            var pReservation = await _context.PReservations
                .Include(p => p.Room)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Reservationsid == id);
            if (pReservation == null)
            {
                return NotFound();
            }

            return View(pReservation);
        }

        // GET: PReservations/Create
        public IActionResult Create()
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            ViewData["Roomid"] = new SelectList(_context.PRooms, "Roomid", "Roomid");
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId");
            return View();
        }

        // POST: PReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reservationsid,CheckInDate,CheckOutDate,Toltalprice,Invoicepdf,UserId,Roomid")] PReservation pReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roomid"] = new SelectList(_context.PRooms, "Roomid", "Roomid", pReservation.Roomid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pReservation.UserId);
            return View(pReservation);
        }

        // GET: PReservations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            if (id == null || _context.PReservations == null)
            {
                return NotFound();
            }

            var pReservation = await _context.PReservations.FindAsync(id);
            if (pReservation == null)
            {
                return NotFound();
            }
            ViewData["Roomid"] = new SelectList(_context.PRooms, "Roomid", "Roomid", pReservation.Roomid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pReservation.UserId);
            return View(pReservation);
        }

        // POST: PReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Reservationsid,CheckInDate,CheckOutDate,Toltalprice,Invoicepdf,UserId,Roomid")] PReservation pReservation)
        {
            if (id != pReservation.Reservationsid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PReservationExists(pReservation.Reservationsid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roomid"] = new SelectList(_context.PRooms, "Roomid", "Roomid", pReservation.Roomid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pReservation.UserId);
            return View(pReservation);
        }

        // GET: PReservations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            if (id == null || _context.PReservations == null)
            {
                return NotFound();
            }

            var pReservation = await _context.PReservations
                .Include(p => p.Room)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Reservationsid == id);
            if (pReservation == null)
            {
                return NotFound();
            }

            return View(pReservation);
        }

        // POST: PReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PReservations == null)
            {
                return Problem("Entity set 'ModelContext.PReservations'  is null.");
            }
            var pReservation = await _context.PReservations.FindAsync(id);
            if (pReservation != null)
            {
                _context.PReservations.Remove(pReservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PReservationExists(decimal id)
        {
          return (_context.PReservations?.Any(e => e.Reservationsid == id)).GetValueOrDefault();
        }
    }
}
