using BessinessLogicLayer.Interfaces;
using DatabaseLogicLayer.Context;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Service
{
    public class NotesRepository : INotesRepository
    {
            private readonly FunDooContext _context;

            public NotesRepository(FunDooContext context)
            {
                _context = context;
            }

            public Note Create(Note note)
            {
                _context.Notes.Add(note);
                _context.SaveChanges();
                return note;
            }

            public IEnumerable<Note> GetAll(int userId)
            {
                return _context.Notes
                    .Where(n => n.UserId == userId && !n.IsTrash)
                    .ToList();
            }

            public Note GetById(int noteId, int userId)
            {
                return _context.Notes
                    .FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
            }

            public void Update(Note note)
            {
                _context.Notes.Update(note);
                _context.SaveChanges();
            }

            public void Delete(Note note)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
    }

}

