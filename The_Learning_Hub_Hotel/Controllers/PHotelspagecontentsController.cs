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
    public class PHotelspagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PHotelspagecontentsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {   
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PHotelspagecontents
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
            var modelContext = _context.PHotelspagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PHotelspagecontents/Details/5
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
            if (id == null || _context.PHotelspagecontents == null)
            {
                return NotFound();
            }

            var pHotelspagecontent = await _context.PHotelspagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Hotelspagecontentid == id);
            if (pHotelspagecontent == null)
            {
                return NotFound();
            }

            return View(pHotelspagecontent);
        }

        // GET: PHotelspagecontents/Create
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid");
            return View();
        }

        // POST: PHotelspagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotelspagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PHotelspagecontent pHotelspagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pHotelspagecontent.ImageFileTop != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pHotelspagecontent.ImageFileTop.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pHotelspagecontent.ImageFileTop.CopyToAsync(fileStream);
                    }

                    pHotelspagecontent.ImagepathTop = fileName;

                }
                _context.Add(pHotelspagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pHotelspagecontent.Userloginid);
            return View(pHotelspagecontent);
        }

        // GET: PHotelspagecontents/Edit/5
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
            if (id == null || _context.PHotelspagecontents == null)
            {
                return NotFound();
            }

            var pHotelspagecontent = await _context.PHotelspagecontents.FindAsync(id);
            if (pHotelspagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pHotelspagecontent.Userloginid);
            return View(pHotelspagecontent);
        }

        // POST: PHotelspagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hotelspagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PHotelspagecontent pHotelspagecontent)
        {
            if (pHotelspagecontent.ImageFileTop != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pHotelspagecontent.ImageFileTop.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pHotelspagecontent.ImageFileTop.CopyToAsync(fileStream);
                }

                pHotelspagecontent.ImagepathTop = fileName;

            }
            if (id != pHotelspagecontent.Hotelspagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pHotelspagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PHotelspagecontentExists(pHotelspagecontent.Hotelspagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pHotelspagecontent.Userloginid);
            return View(pHotelspagecontent);
        }

        // GET: PHotelspagecontents/Delete/5
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
            if (id == null || _context.PHotelspagecontents == null)
            {
                return NotFound();
            }

            var pHotelspagecontent = await _context.PHotelspagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Hotelspagecontentid == id);
            if (pHotelspagecontent == null)
            {
                return NotFound();
            }

            return View(pHotelspagecontent);
        }

        // POST: PHotelspagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PHotelspagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PHotelspagecontents'  is null.");
            }
            var pHotelspagecontent = await _context.PHotelspagecontents.FindAsync(id);
            if (pHotelspagecontent != null)
            {
                _context.PHotelspagecontents.Remove(pHotelspagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PHotelspagecontentExists(decimal id)
        {
          return (_context.PHotelspagecontents?.Any(e => e.Hotelspagecontentid == id)).GetValueOrDefault();
        }
    }
}
