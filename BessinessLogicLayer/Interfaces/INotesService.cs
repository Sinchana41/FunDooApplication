using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Interfaces
{
    public interface INotesService
    {
        Note Create(CreateNoteDto dto, int userId);
        IEnumerable<Note> GetAll(int userId);
        Note Update(int noteId, UpdateNoteDto dto, int userId);
        void MoveToTrash(int noteId, int userId);
    }
}
