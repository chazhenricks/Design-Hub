using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using designhub.Data;
using designhub.Models;
using designhub.Models.DocumentGroupViewModels;


namespace designhub.Controllers
{
    public class DocumentGroupsController : Controller
    {
        // *****************************
        // *****************************
        // CONTEXT SETUP
        // *****************************
        // *****************************


        private readonly ApplicationDbContext _context;

        public DocumentGroupsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        // *****************************
        // *****************************
        // GET CALLS
        // *****************************
        // *****************************

        // GET: DocumentGroups
        //id refers to ProjectID
        public async Task<IActionResult> Index(int id)
        {
            //creates new instance of the DocumentGroupsList
            DocumentGroupsList viewModel = new DocumentGroupsList();

         
            //grabs each document group for a given project id
            var documentGroups = await (
                from d in _context.DocumentGroup
                from pd in _context.ProjectDocumentGroup
                where d.DocumentGroupID == pd.DocumentGroupID
                && pd.ProjectID == id
                select d)
                .OrderByDescending(dg => dg.DateCreated)
                .ToListAsync();

            //grabs an instance of the project
            var project = await _context.Project.SingleAsync(p => p.ProjectID == id);

            //creates a new list of documents 
            List<Document> documents = new List<Document>();



            if (documentGroups == null)
            {
                return NotFound();
            }

            //if there are document groups present
            if (documentGroups.Count > 0)
            {
                //loop through them
                foreach (var dg in documentGroups)
                {
                    //grab any documents associated with a docuemtn group and add it to a list
                    var dgDocuments = await _context.Document
                                    .Where(d => d.DocumentGroupID == dg.DocumentGroupID)
                                    .OrderByDescending(d => d.DateCreated)
                                    .ToListAsync();
                    //go through that list and push to our overall document list
                    foreach (Document doc in dgDocuments)
                    {
                        documents.Add(doc);
                    }
                }
               //builds the viewModel with a project, list of document groups, and list of documents and returns index view with that viewModel
                viewModel.DocumentGroups = documentGroups;
                viewModel.Project = project;
                viewModel.Documents = documents;
                return View(viewModel);
            }

            //if there are no document groups for a given project ID, redirect to create a new document group 
            EmptyProjectViewModel viewModel = new EmptyProjectViewModel();
            return View("EmptyProject", viewModel);

            
        }

       // *****************************
       // POTENTIALLY  NOT USED
       // ******************************

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
        //id refers to Project ID
        public IActionResult Create(int id)
        {
            //Creates a blank document group, but passes Project ID along with it
            CreateDocumentGroupViewModel viewModel = new CreateDocumentGroupViewModel();
            viewModel.ProjectID = id;
            viewModel.DocumentGroup = new DocumentGroup();

            return View(viewModel);
        }

        // *****************************
        // *****************************
        // POST METHODS
        // *****************************
        // *****************************

        // POST: DocumentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateDocumentGroupViewModel viewModel)
        {
            //sets date created to current date
            viewModel.DocumentGroup.DateCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                //sets up ProjectDocumentGroup and assisns ProjectID to it to update as soon as new document group gets created.
                ProjectDocumentGroup newProjectDocumentGroup = new ProjectDocumentGroup();
                newProjectDocumentGroup.ProjectID = viewModel.ProjectID;

                //Adds new DocumentGroup to DB
                _context.Add(viewModel.DocumentGroup);
                await _context.SaveChangesAsync();

                //Assigns the DocumentGroupID of the newly added DocumentGroup to the new ProjectDocumentGroup model and updates the ProjectDocumentGroup join table
                newProjectDocumentGroup.DocumentGroupID = viewModel.DocumentGroup.DocumentGroupID;
                _context.ProjectDocumentGroup.Add(newProjectDocumentGroup);
                await _context.SaveChangesAsync();

                //Once added it redirects user to the index view for current Project
                return RedirectToAction("Index", new { id = viewModel.ProjectID });

            }
            //If model validation fails will redirect back to the index view for current project
            return RedirectToAction("Index", new { id = viewModel.ProjectID});
        }


        // *****************************
        // POTENTIALLY  NOT USED
        // ******************************
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

        // *****************************
        // *****************************
        // Delete Methods
        // *****************************
        // *****************************

        // GET: DocumentGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //grabs current documentGroup to delete
            var documentGroup = await _context.DocumentGroup
                .SingleOrDefaultAsync(m => m.DocumentGroupID == id);
            if (documentGroup == null)
            {
                return NotFound();
            }

            //creates a new instance of DeleteDocumentGroup model, that contains a DocumentGroup and Project ID
            DeleteDocumentGroup viewModel = new DeleteDocumentGroup();


            //grabs Project by dipping into ProjectDocumentGroup join table
            var projectDocGroupList = await (
                   from pdg in _context.ProjectDocumentGroup
                   where pdg.DocumentGroupID == documentGroup.DocumentGroupID
                   select pdg)
                   .ToListAsync();

            //grabs first return
            var pg = projectDocGroupList.First();

            viewModel.DocumentGroup = documentGroup;
            viewModel.ProjectID = pg.ProjectID;

            //Returns delete view with viewModel
            return View(viewModel);
        }

        //Actual delete method
        //id is DocumentGroupID and ProjectID is ProjectID
        // POST: DocumentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int ProjectID)
        {
            var documentGroup = await _context.DocumentGroup.SingleOrDefaultAsync(m => m.DocumentGroupID == id);
            _context.DocumentGroup.Remove(documentGroup);
            await _context.SaveChangesAsync();
            //after delete, redirects to the index page for current Project
            return RedirectToAction("Index" , new { id = ProjectID});
        }

        private bool DocumentGroupExists(int id)
        {
            return _context.DocumentGroup.Any(e => e.DocumentGroupID == id);
        }
    }
}
