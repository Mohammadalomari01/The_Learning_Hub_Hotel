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
    public class PBanksController : Controller
    {
        private readonly ModelContext _context;

        public PBanksController(ModelContext context)
        {
            _context = context;
        }

        // GET: PBanks
        public async Task<IActionResult> Index()
        {
              return _context.PBanks != null ? 
                          View(await _context.PBanks.ToListAsync()) :
                          Problem("Entity set 'ModelContext.PBanks'  is null.");
        }

        // GET: PBanks/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.PBanks == null)
            {
                return NotFound();
            }

            var pBank = await _context.PBanks
                .FirstOrDefaultAsync(m => m.Bankid == id);
            if (pBank == null)
            {
                return NotFound();
            }

            return View(pBank);
        }

        // GET: PBanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PBanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bankid,Creditcardnumber,Money,Creditcardexp")] PBank pBank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pBank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pBank);
        }

        // GET: PBanks/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.PBanks == null)
            {
                return NotFound();
            }

            var pBank = await _context.PBanks.FindAsync(id);
            if (pBank == null)
            {
                return NotFound();
            }
            return View(pBank);
        }

        // POST: PBanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Bankid,Creditcardnumber,Money,Creditcardexp")] PBank pBank)
        {
            if (id != pBank.Bankid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pBank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PBankExists(pBank.Bankid))
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
            return View(pBank);
        }

        // GET: PBanks/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.PBanks == null)
            {
                return NotFound();
            }

            var pBank = await _context.PBanks
                .FirstOrDefaultAsync(m => m.Bankid == id);
            if (pBank == null)
            {
                return NotFound();
            }

            return View(pBank);
        }

        // POST: PBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PBanks == null)
            {
                return Problem("Entity set 'ModelContext.PBanks'  is null.");
            }
            var pBank = await _context.PBanks.FindAsync(id);
            if (pBank != null)
            {
                _context.PBanks.Remove(pBank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PBankExists(decimal id)
        {
          return (_context.PBanks?.Any(e => e.Bankid == id)).GetValueOrDefault();
        }
    }
}
