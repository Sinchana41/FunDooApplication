using DatabaseLogicLayer.Context;
using DatabaseLogicLayer.Repository.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Repository.Implementations
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly FunDooContext _context;
        public CollaboratorRepository(FunDooContext context)
        {
            _context = context;
        }

        public Collaborator Add(Collaborator collaborator)
        {
            _context.Collaborators.Add(collaborator);
            _context.SaveChanges();
            return collaborator;
        }

        public IEnumerable<Collaborator> GetByNoteId(int noteId)
        {
            return _context.Collaborators
                .Where(c => c.NoteId == noteId)
                .ToList();
        }

        public Collaborator GetById(int collaboratorId)
        {
            return _context.Collaborators
                .FirstOrDefault(c => c.CollaboratorId == collaboratorId);
        }

        public Collaborator GetByNoteAndEmail(int noteId, string email)
        {
            return _context.Collaborators
                .FirstOrDefault(c => c.NoteId == noteId && c.Email == email);
        }

        public void Delete(Collaborator collaborator)
        {
            _context.Collaborators.Remove(collaborator);
            _context.SaveChanges();
        }

        public IEnumerable<Note> GetSharedNotes(string email)
        {
            return _context.Collaborators
                .Where(c => c.Email == email)
                .Select(c => c.Note)
                .ToList();
        }
    }
}
