using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POC_ANTO.Models;

namespace POC_ANTO.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly POCContext _context;

        public InvoicesController(POCContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
              return View(await _context.Invoice.ToListAsync());
        }
        public IActionResult InvoiceLineItems(int id)
        {
            //LineItems model = new LineItems();
            var details = (from i in _context.Invoice where i.InvoiceId == id select new Invoice { InvoiceId = i.InvoiceId, Contact = i.Contact, From = i.From, Tax = i.Tax, Billto = i.Billto, SubTotal = i.SubTotal, Amount = i.Amount, Date = i.Date }).FirstOrDefault();

            //model.Details = details;
            var list = (from i in _context.Items.Include(x => x.Invoice) where i.InvoiceID == id select new Item { ItemName = i.ItemName, ItemAmount = i.ItemAmount, UnitCost = i.UnitCost, Quantity = i.Quantity }).ToList();
            //model.List = list;
            ViewBag.data = details;
            ViewBag.item = list;
            return View();
        }
        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,Billto,Contact,Date,Amount,SubTotal,Tax,From")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                if (_context.Invoice.Any(ac => ac.InvoiceId.Equals(invoice.InvoiceId)))
                {
                    ViewBag.Value = "The invoice with the number is already present please check again ";
                }
                else
                {
                    _context.Add(invoice);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Invoice Added Successfully...!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,Billto,Contact,Date,Amount,SubTotal,Tax,From")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invoice == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invoice == null)
            {
                return Problem("Entity set 'POCContext.Invoice'  is null.");
            }
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoice.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult SearchForm()
        {
            return View();
        }
        public IActionResult SearchResult(int SearchPhrase)
        {
            return View(_context.Items.Where(i => i.InvoiceID.Equals(SearchPhrase)).Include(i=>i.Invoice).ToList());
        }
            private bool InvoiceExists(int id)
        {
          return _context.Invoice.Any(e => e.InvoiceId == id);
        }
    }
}
