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
    public class PAboutpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PAboutpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PAboutpagecontents
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
            var modelContext = _context.PAboutpagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PAboutpagecontents/Details/5
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
            if (id == null || _context.PAboutpagecontents == null)
            {
                return NotFound();
            }

            var pAboutpagecontent = await _context.PAboutpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Aboutpagecontentid == id);
            if (pAboutpagecontent == null)
            {
                return NotFound();
            }

            return View(pAboutpagecontent);
        }

        // GET: PAboutpagecontents/Create
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

        // POST: PAboutpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Aboutpagecontentid,Projectname,Pagename,ImageFileTop,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PAboutpagecontent pAboutpagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pAboutpagecontent.ImageFileTop != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pAboutpagecontent.ImageFileTop.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pAboutpagecontent.ImageFileTop.CopyToAsync(fileStream);
                    }

                    pAboutpagecontent.ImagepathTop = fileName;

                }
                _context.Add(pAboutpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pAboutpagecontent.Userloginid);
            return View(pAboutpagecontent);
        }

        // GET: PAboutpagecontents/Edit/5
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
            if (id == null || _context.PAboutpagecontents == null)
            {
                return NotFound();
            }

            var pAboutpagecontent = await _context.PAboutpagecontents.FindAsync(id);
            if (pAboutpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pAboutpagecontent.Userloginid);
            return View(pAboutpagecontent);
        }

        // POST: PAboutpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Aboutpagecontentid,Projectname,Pagename,ImageFileTop,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PAboutpagecontent pAboutpagecontent)
        {
            if (pAboutpagecontent.ImageFileTop != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(pAboutpagecontent.ImageFileTop.FileName);

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pAboutpagecontent.ImageFileTop.CopyToAsync(fileStream);
                }

                pAboutpagecontent.ImagepathTop = fileName;

            }
            if (id != pAboutpagecontent.Aboutpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pAboutpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PAboutpagecontentExists(pAboutpagecontent.Aboutpagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pAboutpagecontent.Userloginid);
            return View(pAboutpagecontent);
        }

        // GET: PAboutpagecontents/Delete/5
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
            if (id == null || _context.PAboutpagecontents == null)
            {
                return NotFound();
            }

            var pAboutpagecontent = await _context.PAboutpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Aboutpagecontentid == id);
            if (pAboutpagecontent == null)
            {
                return NotFound();
            }

            return View(pAboutpagecontent);
        }

        // POST: PAboutpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PAboutpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PAboutpagecontents'  is null.");
            }
            var pAboutpagecontent = await _context.PAboutpagecontents.FindAsync(id);
            if (pAboutpagecontent != null)
            {
                _context.PAboutpagecontents.Remove(pAboutpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PAboutpagecontentExists(decimal id)
        {
          return (_context.PAboutpagecontents?.Any(e => e.Aboutpagecontentid == id)).GetValueOrDefault();
        }
    }
}
