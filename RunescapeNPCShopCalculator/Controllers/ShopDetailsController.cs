using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunescapeNPCShopCalculator.Data;
using RunescapeNPCShopCalculator.Models;

namespace RunescapeNPCShopCalculator.Controllers
{
    public class ShopDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public ShopDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShopDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShopDetails.ToListAsync());
        }

        // GET: ShopDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopDetail = await _context.ShopDetails.Include(x => x.ShopItems)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (shopDetail == null)
            {
                return NotFound();
            }

            return View(shopDetail);
        }

        // GET: ShopDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShopDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Members,Shopkeeper,Notes")] ShopDetail shopDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopDetail);
        }

        // GET: ShopDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopDetail = await _context.ShopDetails.SingleOrDefaultAsync(m => m.Id == id);
            if (shopDetail == null)
            {
                return NotFound();
            }
            return View(shopDetail);
        }

        // POST: ShopDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Members,Shopkeeper,Notes")] ShopDetail shopDetail)
        {
            if (id != shopDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopDetailExists(shopDetail.Id))
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
            return View(shopDetail);
        }

        // GET: ShopDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopDetail = await _context.ShopDetails
                .SingleOrDefaultAsync(m => m.Id == id);
            if (shopDetail == null)
            {
                return NotFound();
            }

            return View(shopDetail);
        }

        // POST: ShopDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopDetail = await _context.ShopDetails.SingleOrDefaultAsync(m => m.Id == id);
            _context.ShopDetails.Remove(shopDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopDetailExists(int id)
        {
            return _context.ShopDetails.Any(e => e.Id == id);
        }
    }
}
