using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Repository.Interfaces
{
    public interface ILabelRepository
    {
        Label1 Create(Label1 label);
        IEnumerable<Label1> GetAll(int userId);
        Label1 GetById(int labelId, int userId);
        void Update(Label1 label);
        void Delete(Label1 label);

        void AddLabelToNote(int labelId, int noteId);
        void RemoveLabelFromNote(int labelId,int noteId);
        IEnumerable<Note> getNotesByLabel(int labelId,int userId);
    }
}
