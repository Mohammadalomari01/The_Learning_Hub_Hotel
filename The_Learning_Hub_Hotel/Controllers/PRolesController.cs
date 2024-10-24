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
    public class PRolesController : Controller
    {
        private readonly ModelContext _context;

        public PRolesController(ModelContext context)
        {
            _context = context;
        }

        // GET: PRoles
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
            return _context.PRoles != null ? 
                          View(await _context.PRoles.ToListAsync()) :
                          Problem("Entity set 'ModelContext.PRoles'  is null.");
        }

        // GET: PRoles/Details/5
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
            if (id == null || _context.PRoles == null)
            {
                return NotFound();
            }

            var pRole = await _context.PRoles
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (pRole == null)
            {
                return NotFound();
            }

            return View(pRole);
        }

        // GET: PRoles/Create
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

        // POST: PRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roleid,Rolename")] PRole pRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pRole);
        }

        // GET: PRoles/Edit/5
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
            if (id == null || _context.PRoles == null)
            {
                return NotFound();
            }

            var pRole = await _context.PRoles.FindAsync(id);
            if (pRole == null)
            {
                return NotFound();
            }
            return View(pRole);
        }

        // POST: PRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roleid,Rolename")] PRole pRole)
        {
            if (id != pRole.Roleid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PRoleExists(pRole.Roleid))
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
            return View(pRole);
        }

        // GET: PRoles/Delete/5
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
            if (id == null || _context.PRoles == null)
            {
                return NotFound();
            }

            var pRole = await _context.PRoles
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (pRole == null)
            {
                return NotFound();
            }

            return View(pRole);
        }

        // POST: PRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PRoles == null)
            {
                return Problem("Entity set 'ModelContext.PRoles'  is null.");
            }
            var pRole = await _context.PRoles.FindAsync(id);
            if (pRole != null)
            {
                _context.PRoles.Remove(pRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PRoleExists(decimal id)
        {
          return (_context.PRoles?.Any(e => e.Roleid == id)).GetValueOrDefault();
        }
    }
}
