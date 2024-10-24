using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using The_Learning_Hub_Hotel.Models;

namespace The_Learning_Hub_Hotel.Controllers
{
    public class PHotelsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PHotelsController(ModelContext context , IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PHotels
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
            return _context.PHotels != null ? 
                          View(await _context.PHotels.ToListAsync()) :
                          Problem("Entity set 'ModelContext.PHotels'  is null.");
        }

        // GET: PHotels/Details/5
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
            if (id == null || _context.PHotels == null)
            {
                return NotFound();
            }

            var pHotel = await _context.PHotels
                .FirstOrDefaultAsync(m => m.Hotelid == id);
            if (pHotel == null)
            {
                return NotFound();
            }

            return View(pHotel);
        }

        // GET: PHotels/Create
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
            return View();
        }

        // POST: PHotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotelid,Hotelname,Location,Description,ImageFile")] PHotel pHotel)
        {
            if (ModelState.IsValid)
            {
                if (pHotel.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pHotel.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pHotel.ImageFile.CopyToAsync(fileStream);
                    }

                    pHotel.Imagepath = fileName;

                }
                _context.Add(pHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pHotel);
        }

        // GET: PHotels/Edit/5
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

            if (id == null || _context.PHotels == null)
            {
                return NotFound();
            }

            var pHotel = await _context.PHotels.FindAsync(id);
            if (pHotel == null)
            {
                return NotFound();
            }
            return View(pHotel);
        }

        // POST: PHotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hotelid,Hotelname,Location,Description,ImageFile")] PHotel pHotel)
        {
            if (pHotel.ImageFile != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pHotel.ImageFile.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pHotel.ImageFile.CopyToAsync(fileStream);
                }

                pHotel.Imagepath = fileName;

            }
            if (id != pHotel.Hotelid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PHotelExists(pHotel.Hotelid))
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
            return View(pHotel);
        }

        // GET: PHotels/Delete/5
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
            if (id == null || _context.PHotels == null)
            {
                return NotFound();
            }

            var pHotel = await _context.PHotels
                .FirstOrDefaultAsync(m => m.Hotelid == id);
            if (pHotel == null)
            {
                return NotFound();
            }

            return View(pHotel);
        }

        // POST: PHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PHotels == null)
            {
                return Problem("Entity set 'ModelContext.PHotels'  is null.");
            }
            var pHotel = await _context.PHotels.FindAsync(id);
            if (pHotel != null)
            {
                _context.PHotels.Remove(pHotel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PHotelExists(decimal id)
        {
          return (_context.PHotels?.Any(e => e.Hotelid == id)).GetValueOrDefault();
        }
    }
}
