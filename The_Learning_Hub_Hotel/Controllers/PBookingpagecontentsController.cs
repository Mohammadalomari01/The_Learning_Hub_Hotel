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
    public class PBookingpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PBookingpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnvironment )
        { 
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PBookingpagecontents
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
            var modelContext = _context.PBookingpagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PBookingpagecontents/Details/5
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
            if (id == null || _context.PBookingpagecontents == null)
            {
                return NotFound();
            }

            var pBookingpagecontent = await _context.PBookingpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Bookingpagecontentid == id);
            if (pBookingpagecontent == null)
            {
                return NotFound();
            }

            return View(pBookingpagecontent);
        }

        // GET: PBookingpagecontents/Create
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

        // POST: PBookingpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bookingpagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PBookingpagecontent pBookingpagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pBookingpagecontent.ImageFileTop != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pBookingpagecontent.ImageFileTop.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pBookingpagecontent.ImageFileTop.CopyToAsync(fileStream);
                    }

                    pBookingpagecontent.ImagepathTop = fileName;

                }
                _context.Add(pBookingpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pBookingpagecontent.Userloginid);
            return View(pBookingpagecontent);
        }

        // GET: PBookingpagecontents/Edit/5
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
            if (id == null || _context.PBookingpagecontents == null)
            {
                return NotFound();
            }

            var pBookingpagecontent = await _context.PBookingpagecontents.FindAsync(id);
            if (pBookingpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pBookingpagecontent.Userloginid);
            return View(pBookingpagecontent);
        }

        // POST: PBookingpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Bookingpagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PBookingpagecontent pBookingpagecontent)
        {
            if (pBookingpagecontent.ImageFileTop != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pBookingpagecontent.ImageFileTop.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pBookingpagecontent.ImageFileTop.CopyToAsync(fileStream);
                }

                pBookingpagecontent.ImagepathTop = fileName;

            }
            if (id != pBookingpagecontent.Bookingpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pBookingpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PBookingpagecontentExists(pBookingpagecontent.Bookingpagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pBookingpagecontent.Userloginid);
            return View(pBookingpagecontent);
        }

        // GET: PBookingpagecontents/Delete/5
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
            if (id == null || _context.PBookingpagecontents == null)
            {
                return NotFound();
            }

            var pBookingpagecontent = await _context.PBookingpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Bookingpagecontentid == id);
            if (pBookingpagecontent == null)
            {
                return NotFound();
            }

            return View(pBookingpagecontent);
        }

        // POST: PBookingpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PBookingpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PBookingpagecontents'  is null.");
            }
            var pBookingpagecontent = await _context.PBookingpagecontents.FindAsync(id);
            if (pBookingpagecontent != null)
            {
                _context.PBookingpagecontents.Remove(pBookingpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PBookingpagecontentExists(decimal id)
        {
          return (_context.PBookingpagecontents?.Any(e => e.Bookingpagecontentid == id)).GetValueOrDefault();
        }
    }
}
