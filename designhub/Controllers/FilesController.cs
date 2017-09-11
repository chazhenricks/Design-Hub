using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using designhub.Data;
using designhub.Models;
using Microsoft.AspNetCore.Identity;

namespace designhub.Controllers
{
    public class FilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public FilesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.File.Include(f => f.FileGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .Include(f => f.FileGroup)
                .SingleOrDefaultAsync(m => m.FileID == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            ViewData["FileGroupID"] = new SelectList(_context.Set<FileGroup>(), "FileGroupID", "Name");
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileID,DateCreated,FilePath,FileGroupID")] File file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(file);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FileGroupID"] = new SelectList(_context.Set<FileGroup>(), "FileGroupID", "Name", file.FileGroupID);
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File.SingleOrDefaultAsync(m => m.FileID == id);
            if (file == null)
            {
                return NotFound();
            }
            ViewData["FileGroupID"] = new SelectList(_context.Set<FileGroup>(), "FileGroupID", "Name", file.FileGroupID);
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileID,DateCreated,FilePath,FileGroupID")] File file)
        {
            if (id != file.FileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.FileID))
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
            ViewData["FileGroupID"] = new SelectList(_context.Set<FileGroup>(), "FileGroupID", "Name", file.FileGroupID);
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .Include(f => f.FileGroup)
                .SingleOrDefaultAsync(m => m.FileID == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var file = await _context.File.SingleOrDefaultAsync(m => m.FileID == id);
            _context.File.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FileExists(int id)
        {
            return _context.File.Any(e => e.FileID == id);
        }
    }
}
