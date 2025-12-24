using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Interfaces
{
    public interface INotesRepository
    {
        Note Create(Note note);
        IEnumerable<Note> GetAll(int userId);
        Note GetById(int noteId, int userId);
        void Update(Note note);
        void Delete(Note note);
    }
}
