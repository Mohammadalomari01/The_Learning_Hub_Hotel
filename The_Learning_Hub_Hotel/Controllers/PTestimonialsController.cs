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
    public class PTestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public PTestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: PTestimonials
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
            var modelContext = _context.PTestimonials.Include(p => p.Hotel).Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: PTestimonials/Details/5
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
            if (id == null || _context.PTestimonials == null)
            {
                return NotFound();
            }

            var pTestimonial = await _context.PTestimonials
                .Include(p => p.Hotel)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Testimonialid == id);
            if (pTestimonial == null)
            {
                return NotFound();
            }

            return View(pTestimonial);
        }

        // GET: PTestimonials/Create
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
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId");
            return View();
        }

        // POST: PTestimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testimonialid,TestimonialText,Rating,CreatedAt,UserId,Hotelid")] PTestimonial pTestimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid", pTestimonial.Hotelid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pTestimonial.UserId);

            //ViewData["Status"] = new SelectList(new[]
            //{
            //   new { Value = "Pending", Text = "Pending" },
            //   new { Value = "Approved", Text = "Approved" },
            //   new { Value = "Rejected", Text = "Rejected" }
            //}, "Value", "Text", pTestimonial.Status);
            return View(pTestimonial);
        }

        // GET: PTestimonials/Edit/5
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
            if (id == null || _context.PTestimonials == null)
            {
                return NotFound();
            }

            var pTestimonial = await _context.PTestimonials.FindAsync(id);
            if (pTestimonial == null)
            {
                return NotFound();
            }
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid", pTestimonial.Hotelid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pTestimonial.UserId);
            return View(pTestimonial);
        }

        // POST: PTestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Testimonialid,TestimonialText,Rating,CreatedAt,UserId,Hotelid")] PTestimonial pTestimonial)
        {
            if (id != pTestimonial.Testimonialid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pTestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PTestimonialExists(pTestimonial.Testimonialid))
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
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid", pTestimonial.Hotelid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pTestimonial.UserId);
            //ViewData["Status"] = new SelectList(new[]
            //  {
            //       new { Value = "Pending", Text = "Pending" },
            //       new { Value = "Approved", Text = "Approved" },
            //       new { Value = "Rejected", Text = "Rejected" }
            //   }, "Value", "Text", pTestimonial.Status);
            return View(pTestimonial);
        }

        // GET: PTestimonials/Delete/5
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
            if (id == null || _context.PTestimonials == null)
            {
                return NotFound();
            }

            var pTestimonial = await _context.PTestimonials
                .Include(p => p.Hotel)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Testimonialid == id);
            if (pTestimonial == null)
            {
                return NotFound();
            }

            return View(pTestimonial);
        }

        // POST: PTestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PTestimonials == null)
            {
                return Problem("Entity set 'ModelContext.PTestimonials'  is null.");
            }
            var pTestimonial = await _context.PTestimonials.FindAsync(id);
            if (pTestimonial != null)
            {
                _context.PTestimonials.Remove(pTestimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PTestimonialExists(decimal id)
        {
          return (_context.PTestimonials?.Any(e => e.Testimonialid == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> ManageTestimonials()
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
            var testimonials = await _context.PTestimonials.ToListAsync();
            if (testimonials == null)
            {
                return View(new List<PTestimonial>()); // أو معالجة الحالة حسب الحاجة
            }
            return View(testimonials);
        }


        //public async Task<IActionResult> ManageTestimonials()
        //{
        //    var testimonials = await _context.PTestimonials.ToListAsync();
        //    return View(testimonials);
        //}



        [HttpPost]
        public async Task<IActionResult> Approve(decimal id)
        {
            var testimonial = await _context.PTestimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.Status = "Approved";
            _context.Update(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageTestimonials)); // أو اسم العرض الخاص بك
        }

        [HttpPost]
        public async Task<IActionResult> Reject(decimal id)
        {
            var testimonial = await _context.PTestimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.Status = "Rejected";
            _context.Update(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageTestimonials)); // أو اسم العرض الخاص بك
        }
    }
}
