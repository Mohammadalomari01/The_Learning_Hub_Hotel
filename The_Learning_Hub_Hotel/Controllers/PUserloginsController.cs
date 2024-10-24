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
    public class PUserloginsController : Controller
    {
        private readonly ModelContext _context;

        public PUserloginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: PUserlogins
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
            var modelContext = _context.PUserlogins.Include(p => p.Role).Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: PUserlogins/Details/5
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
            if (id == null || _context.PUserlogins == null)
            {
                return NotFound();
            }

            var pUserlogin = await _context.PUserlogins
                .Include(p => p.Role)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Userloginid == id);
            if (pUserlogin == null)
            {
                return NotFound();
            }

            return View(pUserlogin);
        }

        // GET: PUserlogins/Create
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
            ViewData["Roleid"] = new SelectList(_context.PRoles, "Roleid", "Roleid");
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId");
            return View();
        }

        // POST: PUserlogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userloginid,Username,Passwordd,Roleid,UserId")] PUserlogin pUserlogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pUserlogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.PRoles, "Roleid", "Roleid", pUserlogin.Roleid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pUserlogin.UserId);
            return View(pUserlogin);
        }

        // GET: PUserlogins/Edit/5
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
            if (id == null || _context.PUserlogins == null)
            {
                return NotFound();
            }

            var pUserlogin = await _context.PUserlogins.FindAsync(id);
            if (pUserlogin == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.PRoles, "Roleid", "Roleid", pUserlogin.Roleid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pUserlogin.UserId);
            return View(pUserlogin);
        }

        // POST: PUserlogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userloginid,Username,Passwordd,Roleid,UserId")] PUserlogin pUserlogin)
        {
            if (id != pUserlogin.Userloginid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pUserlogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PUserloginExists(pUserlogin.Userloginid))
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
            ViewData["Roleid"] = new SelectList(_context.PRoles, "Roleid", "Roleid", pUserlogin.Roleid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pUserlogin.UserId);
            return View(pUserlogin);
        }

        // GET: PUserlogins/Delete/5
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
            if (id == null || _context.PUserlogins == null)
            {
                return NotFound();
            }

            var pUserlogin = await _context.PUserlogins
                .Include(p => p.Role)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Userloginid == id);
            if (pUserlogin == null)
            {
                return NotFound();
            }

            return View(pUserlogin);
        }

        // POST: PUserlogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PUserlogins == null)
            {
                return Problem("Entity set 'ModelContext.PUserlogins'  is null.");
            }
            var pUserlogin = await _context.PUserlogins.FindAsync(id);
            if (pUserlogin != null)
            {
                _context.PUserlogins.Remove(pUserlogin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PUserloginExists(decimal id)
        {
          return (_context.PUserlogins?.Any(e => e.Userloginid == id)).GetValueOrDefault();
        }
    }
}
