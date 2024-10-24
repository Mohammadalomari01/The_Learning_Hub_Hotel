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
    public class PTeampagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PTeampagecontentsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PTeampagecontents
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
            var modelContext = _context.PTeampagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PTeampagecontents/Details/5
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
            if (id == null || _context.PTeampagecontents == null)
            {
                return NotFound();
            }

            var pTeampagecontent = await _context.PTeampagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Teampagecontentid == id);
            if (pTeampagecontent == null)
            {
                return NotFound();
            }

            return View(pTeampagecontent);
        }

        // GET: PTeampagecontents/Create
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

        // POST: PTeampagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Teampagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PTeampagecontent pTeampagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pTeampagecontent.ImageFileTop != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pTeampagecontent.ImageFileTop.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pTeampagecontent.ImageFileTop.CopyToAsync(fileStream);
                    }

                    pTeampagecontent.ImagepathTop = fileName;

                }
                _context.Add(pTeampagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pTeampagecontent.Userloginid);
            return View(pTeampagecontent);
        }

        // GET: PTeampagecontents/Edit/5
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
            if (id == null || _context.PTeampagecontents == null)
            {
                return NotFound();
            }

            var pTeampagecontent = await _context.PTeampagecontents.FindAsync(id);
            if (pTeampagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pTeampagecontent.Userloginid);
            return View(pTeampagecontent);
        }

        // POST: PTeampagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Teampagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PTeampagecontent pTeampagecontent)
        {
            if (pTeampagecontent.ImageFileTop != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pTeampagecontent.ImageFileTop.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pTeampagecontent.ImageFileTop.CopyToAsync(fileStream);
                }

                pTeampagecontent.ImagepathTop = fileName;

            }
            if (id != pTeampagecontent.Teampagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pTeampagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PTeampagecontentExists(pTeampagecontent.Teampagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pTeampagecontent.Userloginid);
            return View(pTeampagecontent);
        }

        // GET: PTeampagecontents/Delete/5
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
            if (id == null || _context.PTeampagecontents == null)
            {
                return NotFound();
            }

            var pTeampagecontent = await _context.PTeampagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Teampagecontentid == id);
            if (pTeampagecontent == null)
            {
                return NotFound();
            }

            return View(pTeampagecontent);
        }

        // POST: PTeampagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PTeampagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PTeampagecontents'  is null.");
            }
            var pTeampagecontent = await _context.PTeampagecontents.FindAsync(id);
            if (pTeampagecontent != null)
            {
                _context.PTeampagecontents.Remove(pTeampagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PTeampagecontentExists(decimal id)
        {
          return (_context.PTeampagecontents?.Any(e => e.Teampagecontentid == id)).GetValueOrDefault();
        }
    }
}
