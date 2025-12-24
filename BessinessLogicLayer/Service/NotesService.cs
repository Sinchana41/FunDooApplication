using BessinessLogicLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Service
{
        public class NotesService : INotesService
        {
            private readonly INotesRepository _repo;

            public NotesService(INotesRepository repo)
            {
                _repo = repo;
            }

            public Note Create(CreateNoteDto dto, int userId)
            {
                var note = new Note
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    Reminder = dto.Reminder,
                    Colour = dto.Colour ?? "#FFFFFF",
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                return _repo.Create(note);
            }

            public IEnumerable<Note> GetAll(int userId)
                => _repo.GetAll(userId);

            public Note Update(int noteId, UpdateNoteDto dto, int userId)
            {
                var note = _repo.GetById(noteId, userId);
                if (note == null) throw new Exception("Note not found");

                note.Title = dto.Title;
                note.Description = dto.Description;
                note.Reminder = dto.Reminder;
                note.Colour = dto.Colour;
                note.UpdatedAt = DateTime.UtcNow;

                _repo.Update(note);
                return note;
            }

            public void MoveToTrash(int noteId, int userId)
            {
                var note = _repo.GetById(noteId, userId);
                note.IsTrash = true;
                _repo.Update(note);
            }
        }

}

