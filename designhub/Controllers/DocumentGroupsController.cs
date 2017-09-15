using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using designhub.Data;
using designhub.Models;

namespace designhub.Controllers
{
    public class DocumentGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentGroupsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: DocumentGroups
        public async Task<IActionResult> Index(int? id)
        {


            var documentGroups = await (
                from d in _context.DocumentGroup
                from pd in _context.ProjectDocumentGroup
                where d.DocumentGroupID == pd.DocumentGroupID
                && pd.ProjectID == id
                select d).ToListAsync();


            if (documentGroups == null)
            {
                return NotFound();
            }

            return View(documentGroups);
        }

        // GET: DocumentGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var documentGroup = await _context.DocumentGroup
                .SingleOrDefaultAsync(m => m.DocumentGroupID == id);
                

            if (documentGroup == null)
            {
                return NotFound();
            }

            return View(documentGroup);
        }

        // GET: DocumentGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentGroupID,DateCreated,Name")] DocumentGroup documentGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(documentGroup);
        }

        // GET: DocumentGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentGroup = await _context.DocumentGroup.SingleOrDefaultAsync(m => m.DocumentGroupID == id);
            if (documentGroup == null)
            {
                return NotFound();
            }
            return View(documentGroup);
        }

        // POST: DocumentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentGroupID,DateCreated,Name")] DocumentGroup documentGroup)
        {
            if (id != documentGroup.DocumentGroupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentGroupExists(documentGroup.DocumentGroupID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(documentGroup);
        }

        // GET: DocumentGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentGroup = await _context.DocumentGroup
                .SingleOrDefaultAsync(m => m.DocumentGroupID == id);
            if (documentGroup == null)
            {
                return NotFound();
            }

            return View(documentGroup);
        }

        // POST: DocumentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentGroup = await _context.DocumentGroup.SingleOrDefaultAsync(m => m.DocumentGroupID == id);
            _context.DocumentGroup.Remove(documentGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DocumentGroupExists(int id)
        {
            return _context.DocumentGroup.Any(e => e.DocumentGroupID == id);
        }
    }
}