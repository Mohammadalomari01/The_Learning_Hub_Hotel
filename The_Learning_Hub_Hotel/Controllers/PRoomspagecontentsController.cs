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
    public class PRoomspagecontentsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PRoomspagecontentsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
              _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: PRoomspagecontents
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
            var modelContext = _context.PRoomspagecontents.Include(p => p.Userlogin);
            return View(await modelContext.ToListAsync());
        }

        // GET: PRoomspagecontents/Details/5
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
            if (id == null || _context.PRoomspagecontents == null)
            {
                return NotFound();
            }

            var pRoomspagecontent = await _context.PRoomspagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Roomspagecontentid == id);
            if (pRoomspagecontent == null)
            {
                return NotFound();
            }

            return View(pRoomspagecontent);
        }

        // GET: PRoomspagecontents/Create
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

        // POST: PRoomspagecontents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roomspagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PRoomspagecontent pRoomspagecontent)
        {
            if (ModelState.IsValid)
            {
                if (pRoomspagecontent.ImageFileTop != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + pRoomspagecontent.ImageFileTop.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pRoomspagecontent.ImageFileTop.CopyToAsync(fileStream);
                    }

                    pRoomspagecontent.ImagepathTop = fileName;

                }
                _context.Add(pRoomspagecontent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pRoomspagecontent.Userloginid);
            return View(pRoomspagecontent);
        }

        // GET: PRoomspagecontents/Edit/5
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
            if (id == null || _context.PRoomspagecontents == null)
            {
                return NotFound();
            }

            var pRoomspagecontent = await _context.PRoomspagecontents.FindAsync(id);
            if (pRoomspagecontent == null)
            {
                return NotFound();
            }
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pRoomspagecontent.Userloginid);
            return View(pRoomspagecontent);
        }

        // POST: PRoomspagecontents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roomspagecontentid,Projectname,Pagename,ImageFileTop,Footerlocation,Footerphonenumber,Footeremail,Userloginid")] PRoomspagecontent pRoomspagecontent)
        {
            if (pRoomspagecontent.ImageFileTop != null)
            {
                // 1- get rootpath

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //2- filename
                //dhchcvhcbdjcnbhcbhc_Aseel.png
                //wiueyrueiryeuirueiori_Aseel.png
                string fileName = Guid.NewGuid().ToString() + "_" + pRoomspagecontent.ImageFileTop.FileName;

                //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //4- upload image to folder images  
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pRoomspagecontent.ImageFileTop.CopyToAsync(fileStream);
                }

                pRoomspagecontent.ImagepathTop = fileName;

            }
            if (id != pRoomspagecontent.Roomspagecontentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pRoomspagecontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PRoomspagecontentExists(pRoomspagecontent.Roomspagecontentid))
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
            ViewData["Userloginid"] = new SelectList(_context.PUserlogins, "Userloginid", "Userloginid", pRoomspagecontent.Userloginid);
            return View(pRoomspagecontent);
        }

        // GET: PRoomspagecontents/Delete/5
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
            if (id == null || _context.PRoomspagecontents == null)
            {
                return NotFound();
            }

            var pRoomspagecontent = await _context.PRoomspagecontents
                .Include(p => p.Userlogin)
                .FirstOrDefaultAsync(m => m.Roomspagecontentid == id);
            if (pRoomspagecontent == null)
            {
                return NotFound();
            }

            return View(pRoomspagecontent);
        }

        // POST: PRoomspagecontents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PRoomspagecontents == null)
            {
                return Problem("Entity set 'ModelContext.PRoomspagecontents'  is null.");
            }
            var pRoomspagecontent = await _context.PRoomspagecontents.FindAsync(id);
            if (pRoomspagecontent != null)
            {
                _context.PRoomspagecontents.Remove(pRoomspagecontent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PRoomspagecontentExists(decimal id)
        {
          return (_context.PRoomspagecontents?.Any(e => e.Roomspagecontentid == id)).GetValueOrDefault();
        }
    }
}
