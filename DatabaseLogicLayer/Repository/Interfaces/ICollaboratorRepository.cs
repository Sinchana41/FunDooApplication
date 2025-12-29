using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Repository.Interfaces
{
    public interface ICollaboratorRepository
    {
        Collaborator Add(Collaborator collaborator);
        IEnumerable<Collaborator> GetByNoteId(int noteId);
        void Delete(Collaborator collaborator);
        Collaborator GetById(int collaboratorId);
        Collaborator GetByNoteAndEmail(int noteId, string email);
        IEnumerable<Note> GetSharedNotes(string email);
    }
}
