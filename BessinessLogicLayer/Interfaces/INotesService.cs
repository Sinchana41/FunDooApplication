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
        Task<Note> CreateAsync(CreateNoteDto dto, int userId);
        Task<IEnumerable<Note>> GetAllAsync(int userId);
        Task<Note> UpdateAsync(int noteId, UpdateNoteDto dto, int userId);
        Task MoveToTrashAsync(int noteId, int userId);
    }
}
