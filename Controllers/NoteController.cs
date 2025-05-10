using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyNotesApp.Data;
using MyNotesApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace MyNotesApp.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly MyNotesAppContext _context;

        public NoteController(MyNotesAppContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var myNotesAppContext = _context.NoteModel.Include(n => n.User);
            return View(await myNotesAppContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteModel = await _context.NoteModel
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (noteModel == null)
            {
                return NotFound();
            }

            return View(noteModel);
        }

        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.UserModel, "UserId", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,Title,Content,UserId")] NoteModel noteModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteModel);
                await _context.SaveChangesAsync();
                TempData["NoteCreated"] = true;
                return RedirectToAction(nameof(Create));
            }
            ViewData["UserId"] = new SelectList(_context.UserModel, "UserId", "Email", noteModel.UserId);
            return View(noteModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteModel = await _context.NoteModel.FindAsync(id);
            if (noteModel == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserModel, "UserId", "Email", noteModel.UserId);
            return View(noteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,Title,Content,UserId")] NoteModel noteModel)
        {
            if (id != noteModel.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteModelExists(noteModel.NoteId))
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
            ViewData["UserId"] = new SelectList(_context.UserModel, "UserId", "Email", noteModel.UserId);
            return View(noteModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteModel = await _context.NoteModel
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (noteModel == null)
            {
                return NotFound();
            }

            return View(noteModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteModel = await _context.NoteModel.FindAsync(id);
            if (noteModel != null)
            {
                _context.NoteModel.Remove(noteModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteModelExists(int id)
        {
            return _context.NoteModel.Any(e => e.NoteId == id);
        }
    }
}
