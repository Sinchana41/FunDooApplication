using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Interfaces
{
    public interface ILabelService
    {
        Label1 Create(CreateLabelDto dto, int userId);
        IEnumerable<Label1> GetAll(int userId);
        Label1 Update(int labelId, UpdateLabelDto dto, int userId);
        void Delete(int labelId, int userId);

        void AddLabelToNote(int labelId, int noteId, int userId);
        void RemoveLabelFromNote(int labelId, int noteId, int userId);
        IEnumerable<Note> GetNotesByLabel(int labelId, int userId);
    }
}
