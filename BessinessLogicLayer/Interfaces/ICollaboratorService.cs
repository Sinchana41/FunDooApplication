using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Interfaces
{
    public interface ICollaboratorService
    {
        Collaborator Add(AddCollaboratorDto dto, int userId);
        IEnumerable<Collaborator> GetByNoteId(int noteId, int userId);
        void Delete(int collaboratorId, int userId);
        void DeleteByEmail(int noteId, string email, int userId);
        IEnumerable<Note> GetSharedNotes(string email);

    }
}
