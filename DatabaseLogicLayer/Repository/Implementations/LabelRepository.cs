using DatabaseLogicLayer.Context;
using DatabaseLogicLayer.Repository.Interfaces;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Repository.Implementations
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FunDooContext _context;

        public LabelRepository(FunDooContext context)
        {
            _context = context;
        }

        // CREATE LABEL
        public Label1 Create(Label1 label)
        {
            _context.Set<Label1>().Add(label);
            _context.SaveChanges();
            return label;
        }

        // GET ALL LABELS FOR USER
        public IEnumerable<Label1> GetAll(int userId)
        {
            return _context.Set<Label1>()
                .Where(l => l.UserId == userId)
                .ToList();
        }

        // GET LABEL BY ID
        public Label1 GetById(int labelId, int userId)
        {
            return _context.Set<Label1>()
                .FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);
        }

        // UPDATE LABEL
        public void Update(Label1 label)
        {
            _context.Set<Label1>().Update(label);
            _context.SaveChanges();
        }

        // DELETE LABEL
        public void Delete(Label1 label)
        {
            _context.Set<Label1>().Remove(label);
            _context.SaveChanges();
        }

        // ADD LABEL TO NOTE (MANY TO MANY)
        public void AddLabelToNote(int labelId, int noteId)
        {
            var noteLabel = new NoteLabel
            {
                LabelId = labelId,
                NotesId = noteId
            };

            _context.NoteLabels.Add(noteLabel);
            _context.SaveChanges();
        }

        // REMOVE LABEL FROM NOTE
        public void RemoveLabelFromNote(int labelId, int noteId)
        {
            var noteLabel = _context.NoteLabels
                .FirstOrDefault(nl => nl.LabelId == labelId && nl.NotesId == noteId);

            if (noteLabel != null)
            {
                _context.NoteLabels.Remove(noteLabel);
                _context.SaveChanges();
            }
        }

        // GET NOTES BY LABEL
        public IEnumerable<Note> getNotesByLabel(int labelId, int userId)
        {
            return _context.NoteLabels
                .Where(nl => nl.LabelId == labelId && nl.Note.UserId == userId)
                .Select(nl => nl.Note)
                .ToList();
        }
    }
}
