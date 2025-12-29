using BessinessLogicLayer.Interfaces;
using DatabaseLogicLayer.Repository.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Service
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _repo;

        public LabelService(ILabelRepository repo)
        {
            _repo = repo;
        }

        // CREATE LABEL
        public Label1 Create(CreateLabelDto dto, int userId)
        {
            var label = new Label1
            {
                LabelName = dto.LabelName,
                UserId = userId
            };

            return _repo.Create(label);
        }

        // GET ALL LABELS
        public IEnumerable<Label1> GetAll(int userId)
        {
            return _repo.GetAll(userId);
        }

        // UPDATE LABEL
        public Label1 Update(int labelId, UpdateLabelDto dto, int userId)
        {
            var label = _repo.GetById(labelId, userId);
            if (label == null)
                throw new Exception("Label not found");

            label.LabelName = dto.LabelName;

            _repo.Update(label);
            return label;
        }

        // DELETE LABEL
        public void Delete(int labelId, int userId)
        {
            var label = _repo.GetById(labelId, userId);
            if (label == null)
                throw new Exception("Label not found");

            _repo.Delete(label);
        }

        // ADD LABEL TO NOTE
        public void AddLabelToNote(int labelId, int noteId, int userId)
        {
            // (Optional validation can be added later)
            _repo.AddLabelToNote(labelId, noteId);
        }

        // REMOVE LABEL FROM NOTE
        public void RemoveLabelFromNote(int labelId, int noteId, int userId)
        {
            _repo.RemoveLabelFromNote(labelId, noteId);
        }

        // GET NOTES BY LABEL
        public IEnumerable<Note> GetNotesByLabel(int labelId, int userId)
        {
            return _repo.getNotesByLabel(labelId, userId);
        }
    }
}
