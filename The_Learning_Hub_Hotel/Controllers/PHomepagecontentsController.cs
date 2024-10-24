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
    public class PHomepagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PHomepagecontentsController(ModelContext context , IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PHomepagecontents
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
            var modelContext = _context.PHomepagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PHomepagecontents/Details/5
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
            if (id == null || _context.PHomepagecontents == null)
            {
                return NotFound();
            }

            var pHomepagecontent = await _context.PHomepagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Homepagecontent == id);
            if (pHomepagecontent == null)
            {
                return NotFound();
            }

            return View(pHomepagecontent);
        }

        // GET: PHomepagecontents/Create
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

        // POST: PHomepagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Homepagecontent,Projectname,Pagename,ImageFileTop1,ImageFileTop2,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PHomepagecontent pHomepagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pHomepagecontent.ImageFileTop1 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pHomepagecontent.ImageFileTop1.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pHomepagecontent.ImageFileTop1.CopyToAsync(fileStream);
                    }

                    pHomepagecontent.ImagepathTop1 = fileName;

                }
                if (pHomepagecontent.ImageFileTop2 != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pHomepagecontent.ImageFileTop2.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pHomepagecontent.ImageFileTop2.CopyToAsync(fileStream);
                    }

                    pHomepagecontent.ImagepathTop2 = fileName;

                }
                _context.Add(pHomepagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pHomepagecontent.Userloginid);
            return View(pHomepagecontent);
        }

        // GET: PHomepagecontents/Edit/5
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
            if (id == null || _context.PHomepagecontents == null)
            {
                return NotFound();
            }

            var pHomepagecontent = await _context.PHomepagecontents.FindAsync(id);
            if (pHomepagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pHomepagecontent.Userloginid);
            return View(pHomepagecontent);
        }

        // POST: PHomepagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Homepagecontent,Projectname,Pagename,ImageFileTop1,ImageFileTop2,WelcomeText,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PHomepagecontent pHomepagecontent)
        {
            if (pHomepagecontent.ImageFileTop1 != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pHomepagecontent.ImageFileTop1.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pHomepagecontent.ImageFileTop1.CopyToAsync(fileStream);
                }

                pHomepagecontent.ImagepathTop1 = fileName;

            }
            if (pHomepagecontent.ImageFileTop2 != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pHomepagecontent.ImageFileTop2.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pHomepagecontent.ImageFileTop2.CopyToAsync(fileStream);
                }

                pHomepagecontent.ImagepathTop2 = fileName;

            }
            if (id != pHomepagecontent.Homepagecontent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pHomepagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PHomepagecontentExists(pHomepagecontent.Homepagecontent))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pHomepagecontent.Userloginid);
            return View(pHomepagecontent);
        }

        // GET: PHomepagecontents/Delete/5
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
            if (id == null || _context.PHomepagecontents == null)
            {
                return NotFound();
            }

            var pHomepagecontent = await _context.PHomepagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Homepagecontent == id);
            if (pHomepagecontent == null)
            {
                return NotFound();
            }

            return View(pHomepagecontent);
        }

        // POST: PHomepagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PHomepagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PHomepagecontents'  is null.");
            }
            var pHomepagecontent = await _context.PHomepagecontents.FindAsync(id);
            if (pHomepagecontent != null)
            {
                _context.PHomepagecontents.Remove(pHomepagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PHomepagecontentExists(decimal id)
        {
          return (_context.PHomepagecontents?.Any(e => e.Homepagecontent == id)).GetValueOrDefault();
        }
    }
}
