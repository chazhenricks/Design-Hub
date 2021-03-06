using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using designhub.Data;
using designhub.Models;
using designhub.Models.DocumentViewModels;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace designhub.Controllers
{
    public class DocumentsController : Controller
    {
        // *****************************
        // *****************************
        // CONTEXT SETUP
        // *****************************
        // *****************************


        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DocumentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // *****************************
        // *****************************
        // GET METHODS
        // *****************************
        // *****************************

        // GET: Documents
        //id is DocumentGroupID used to populate list of documents attached to a documentgroup
        public async Task<IActionResult> Index(int id)
        {
            DocumentListViewModel viewModel = new DocumentListViewModel();

            var documents = await (
               from d in _context.Document
               from dg in _context.DocumentGroup
               where d.DocumentGroupID == dg.DocumentGroupID
               && dg.DocumentGroupID == id
               select d)
               .OrderByDescending(d => d.DateCreated)
               .ToListAsync();

            var documentGroup = await _context.DocumentGroup.SingleAsync(dg => dg.DocumentGroupID == id);

            if (documents.Count < 1)
            {
                
                return RedirectToAction("Create", new { id = id});
            }

            viewModel.Documents = documents;
            viewModel.DocumentGroup = documentGroup;

            return View(viewModel);
        }


        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            //grab document that matches the documentID we pass in
            var document = await _context.Document
                .Include(d => d.DocumentGroup)
                .SingleOrDefaultAsync(m => m.DocumentID == id);
            if (document == null)
            {
                return NotFound();
            }

            //grab list of comments that are attached to a documentID
            var comments = await (
               from c in _context.Comment
               where c.DocumentID == document.DocumentID
               select c)
               .ToListAsync();

            //if no comments, return view with no comments attached.
            if (comments.Count < 1)
            {
                return View("DocumentDetailNoComments", document);
            }

            //else build a new DocumentDetailView and return a view with that object attached. 
            DocumentDetailView documentDetail = new DocumentDetailView();
            documentDetail.Document = document;
            documentDetail.Comments = comments;


            return View("DocumentDetailComments",documentDetail);
        }

        // GET: Documents/Create
        public IActionResult Create(int id)
        {
            var createDoc = new Document();

            createDoc.DocumentGroupID = id;
          
            return View(createDoc);
        }


        public FileResult Download(int id)
        {
            var document =  _context.Document
                            .SingleOrDefault(d => d.DocumentID == id);


            var filePath = document.DocumentPath;
            string fileName = filePath.Replace("/documents/", "");
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"wwwroot\" + filePath);
            return File(fileBytes, "application/x-msdownload", fileName);
            
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDocumentViewModel viewModel)

        {

            // Remove the user from the model validation because it is
            // not information posted in the form

            ModelState.Remove("Document.User");
            if (viewModel.NewDocument == null)
            {
                var createDoc = new Document();

                createDoc.DocumentGroupID = viewModel.DocumentGroupID;
                return View("EmptyDocument", createDoc);
            }

            if (ModelState.IsValid)
            {
                /*
                    If all other properties validate, then grab the 
                    currently authenticated user and assign it to the 
                    product before adding it to the db _context
                */
                var user = await GetCurrentUserAsync();


               viewModel.Document = new Document();


                viewModel.Document.User = user;
                viewModel.Document.DateCreated = DateTime.Now;
                viewModel.Document.DocumentGroupID = viewModel.DocumentGroupID;
                

                if (viewModel.NewDocument != null)
                {
                    string directory = Directory.GetCurrentDirectory();
                    string localSavePath = directory + @"\wwwroot\documents\" + viewModel.NewDocument.FileName;
                    string dbPath = "/documents/" + viewModel.NewDocument.FileName;
                    using (var stream = new FileStream(localSavePath, FileMode.Create))
                    {
                        await viewModel.NewDocument.CopyToAsync(stream);
                    }
                    viewModel.Document.DocumentPath = dbPath;
                }




                _context.Add(viewModel.Document);
                await _context.SaveChangesAsync();


                var projectDocGroupList = await (
                    from pdg in _context.ProjectDocumentGroup
                    where pdg.DocumentGroupID == viewModel.DocumentGroupID
                    select pdg)
                    .ToListAsync();


                var pg = projectDocGroupList.First();
                              




                return RedirectToAction("Index", "DocumentGroups", new {id = pg.ProjectID});
            }
           ViewData["DocumentGroupID"] = new SelectList(_context.DocumentGroup, "DocumentGroupID", "Name", viewModel.Document.DocumentGroupID);
            return View(viewModel);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document.SingleOrDefaultAsync(m => m.DocumentID == id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["DocumentGroupID"] = new SelectList(_context.DocumentGroup, "DocumentGroupID", "Name", document.DocumentGroupID);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentID,DateCreated,DocumentPath,DocumentGroupID")] Document document)
        {
            if (id != document.DocumentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.DocumentID))
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
            ViewData["DocumentGroupID"] = new SelectList(_context.DocumentGroup, "DocumentGroupID", "Name", document.DocumentGroupID);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id, int projectID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .Include(d => d.DocumentGroup)
                .SingleOrDefaultAsync(m => m.DocumentID == id);
            if (document == null)
            {
                return NotFound();
            }
            DeleteDocumentViewModel viewModel = new DeleteDocumentViewModel();
            viewModel.Document = document;
            viewModel.ProjectID = projectID;
            return View(viewModel);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Document.SingleOrDefaultAsync(m => m.DocumentID == id);
            _context.Document.Remove(document);
            await _context.SaveChangesAsync();

            var projectDocGroupList = await (
                    from pdg in _context.ProjectDocumentGroup
                    where pdg.DocumentGroupID == document.DocumentGroupID
                    select pdg)
                    .ToListAsync();


            var pg = projectDocGroupList.First();
            return RedirectToAction("Index", "DocumentGroups", new {id = pg.ProjectID});
        }

        private bool DocumentExists(int id)
        {
            return _context.Document.Any(e => e.DocumentID == id);
        }
    }
}
