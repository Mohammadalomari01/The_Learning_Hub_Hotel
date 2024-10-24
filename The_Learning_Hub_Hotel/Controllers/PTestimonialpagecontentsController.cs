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
    public class PTestimonialpagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PTestimonialpagecontentsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PTestimonialpagecontents
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
            var modelContext = _context.PTestimonialpagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PTestimonialpagecontents/Details/5
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
            if (id == null || _context.PTestimonialpagecontents == null)
            {
                return NotFound();
            }

            var pTestimonialpagecontent = await _context.PTestimonialpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Testimonialpagecontentid == id);
            if (pTestimonialpagecontent == null)
            {
                return NotFound();
            }

            return View(pTestimonialpagecontent);
        }

        // GET: PTestimonialpagecontents/Create
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

        // POST: PTestimonialpagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testimonialpagecontentid,Projectname,Pagename,ImageFileTop,ImageFileMiddle,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PTestimonialpagecontent pTestimonialpagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pTestimonialpagecontent.ImageFileTop != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pTestimonialpagecontent.ImageFileTop.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pTestimonialpagecontent.ImageFileTop.CopyToAsync(fileStream);
                    }

                    pTestimonialpagecontent.ImagepathTop = fileName;

                }
                if (pTestimonialpagecontent.ImageFileMiddle != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pTestimonialpagecontent.ImageFileMiddle.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pTestimonialpagecontent.ImageFileMiddle.CopyToAsync(fileStream);
                    }

                    pTestimonialpagecontent.ImagepathMiddle = fileName;

                }
                _context.Add(pTestimonialpagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pTestimonialpagecontent.Userloginid);
            return View(pTestimonialpagecontent);
        }

        // GET: PTestimonialpagecontents/Edit/5
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
            if (id == null || _context.PTestimonialpagecontents == null)
            {
                return NotFound();
            }

            var pTestimonialpagecontent = await _context.PTestimonialpagecontents.FindAsync(id);
            if (pTestimonialpagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pTestimonialpagecontent.Userloginid);
            return View(pTestimonialpagecontent);
        }

        // POST: PTestimonialpagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Testimonialpagecontentid,Projectname,Pagename,ImageFileTop,ImageFileMiddle,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PTestimonialpagecontent pTestimonialpagecontent)
        {
            if (pTestimonialpagecontent.ImageFileTop != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pTestimonialpagecontent.ImageFileTop.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pTestimonialpagecontent.ImageFileTop.CopyToAsync(fileStream);
                }

                pTestimonialpagecontent.ImagepathTop = fileName;

            }
            if (pTestimonialpagecontent.ImageFileMiddle != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pTestimonialpagecontent.ImageFileMiddle.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pTestimonialpagecontent.ImageFileMiddle.CopyToAsync(fileStream);
                }

                pTestimonialpagecontent.ImagepathMiddle = fileName;

            }
            if (id != pTestimonialpagecontent.Testimonialpagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pTestimonialpagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PTestimonialpagecontentExists(pTestimonialpagecontent.Testimonialpagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pTestimonialpagecontent.Userloginid);
            return View(pTestimonialpagecontent);
        }

        // GET: PTestimonialpagecontents/Delete/5
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
            if (id == null || _context.PTestimonialpagecontents == null)
            {
                return NotFound();
            }

            var pTestimonialpagecontent = await _context.PTestimonialpagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Testimonialpagecontentid == id);
            if (pTestimonialpagecontent == null)
            {
                return NotFound();
            }

            return View(pTestimonialpagecontent);
        }

        // POST: PTestimonialpagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PTestimonialpagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PTestimonialpagecontents'  is null.");
            }
            var pTestimonialpagecontent = await _context.PTestimonialpagecontents.FindAsync(id);
            if (pTestimonialpagecontent != null)
            {
                _context.PTestimonialpagecontents.Remove(pTestimonialpagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PTestimonialpagecontentExists(decimal id)
        {
          return (_context.PTestimonialpagecontents?.Any(e => e.Testimonialpagecontentid == id)).GetValueOrDefault();
        }
    }
}
