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
    public class PContactpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PContactpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PContactpagecontents
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
            var modelContext = _context.PContactpagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PContactpagecontents/Details/5
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
            if (id == null || _context.PContactpagecontents == null)
            {
                return NotFound();
            }

            var pContactpagecontent = await _context.PContactpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Contactpagecontentid == id);
            if (pContactpagecontent == null)
            {
                return NotFound();
            }

            return View(pContactpagecontent);
        }

        // GET: PContactpagecontents/Create
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

        // POST: PContactpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Contactpagecontentid,Projectname,Pagename,ImageFileTop,Bookingemail,Generalemail,Technicalemail,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PContactpagecontent pContactpagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pContactpagecontent.ImageFileTop != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pContactpagecontent.ImageFileTop.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pContactpagecontent.ImageFileTop.CopyToAsync(fileStream);
                    }

                    pContactpagecontent.ImagepathTop = fileName;

                }
                _context.Add(pContactpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pContactpagecontent.Userloginid);
            return View(pContactpagecontent);
        }

        // GET: PContactpagecontents/Edit/5
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
            if (id == null || _context.PContactpagecontents == null)
            {
                return NotFound();
            }

            var pContactpagecontent = await _context.PContactpagecontents.FindAsync(id);
            if (pContactpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pContactpagecontent.Userloginid);
            return View(pContactpagecontent);
        }

        // POST: PContactpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Contactpagecontentid,Projectname,Pagename,ImageFileTop,Bookingemail,Generalemail,Technicalemail,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PContactpagecontent pContactpagecontent)
        {
            if (pContactpagecontent.ImageFileTop != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pContactpagecontent.ImageFileTop.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pContactpagecontent.ImageFileTop.CopyToAsync(fileStream);
                }

                pContactpagecontent.ImagepathTop = fileName;

            }
            if (id != pContactpagecontent.Contactpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pContactpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PContactpagecontentExists(pContactpagecontent.Contactpagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pContactpagecontent.Userloginid);
            return View(pContactpagecontent);
        }

        // GET: PContactpagecontents/Delete/5
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
            if (id == null || _context.PContactpagecontents == null)
            {
                return NotFound();
            }

            var pContactpagecontent = await _context.PContactpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Contactpagecontentid == id);
            if (pContactpagecontent == null)
            {
                return NotFound();
            }

            return View(pContactpagecontent);
        }

        // POST: PContactpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PContactpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PContactpagecontents'  is null.");
            }
            var pContactpagecontent = await _context.PContactpagecontents.FindAsync(id);
            if (pContactpagecontent != null)
            {
                _context.PContactpagecontents.Remove(pContactpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PContactpagecontentExists(decimal id)
        {
          return (_context.PContactpagecontents?.Any(e => e.Contactpagecontentid == id)).GetValueOrDefault();
        }
    }
}
