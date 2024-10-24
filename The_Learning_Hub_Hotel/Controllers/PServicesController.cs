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
    public class PServicesController : Controller
    {
        private readonly ModelContext _context;

        public PServicesController(ModelContext context)
        {
            _context = context;
        }

        // GET: PServices
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
            var modelContext = _context.PServices.Include(p => p.Hotel);
            return View(await modelContext.ToListAsync());
        }

        // GET: PServices/Details/5
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
            if (id == null || _context.PServices == null)
            {
                return NotFound();
            }

            var pService = await _context.PServices
                .Include(p => p.Hotel)
                .FirstOrDefaultAsync(m => m.Serviceid == id);
            if (pService == null)
            {
                return NotFound();
            }

            return View(pService);
        }

        // GET: PServices/Create
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
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid");
            return View();
        }

        // POST: PServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Serviceid,Servicename,Servicetext,Hotelid")] PService pService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid", pService.Hotelid);
            return View(pService);
        }

        // GET: PServices/Edit/5
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
            if (id == null || _context.PServices == null)
            {
                return NotFound();
            }

            var pService = await _context.PServices.FindAsync(id);
            if (pService == null)
            {
                return NotFound();
            }
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid", pService.Hotelid);
            return View(pService);
        }

        // POST: PServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Serviceid,Servicename,Servicetext,Hotelid")] PService pService)
        {
            if (id != pService.Serviceid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PServiceExists(pService.Serviceid))
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
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid", pService.Hotelid);
            return View(pService);
        }

        // GET: PServices/Delete/5
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
            if (id == null || _context.PServices == null)
            {
                return NotFound();
            }

            var pService = await _context.PServices
                .Include(p => p.Hotel)
                .FirstOrDefaultAsync(m => m.Serviceid == id);
            if (pService == null)
            {
                return NotFound();
            }

            return View(pService);
        }

        // POST: PServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PServices == null)
            {
                return Problem("Entity set 'ModelContext.PServices'  is null.");
            }
            var pService = await _context.PServices.FindAsync(id);
            if (pService != null)
            {
                _context.PServices.Remove(pService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PServiceExists(decimal id)
        {
          return (_context.PServices?.Any(e => e.Serviceid == id)).GetValueOrDefault();
        }
    }
}
