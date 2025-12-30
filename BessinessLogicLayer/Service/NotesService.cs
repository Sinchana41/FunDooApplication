using BessinessLogicLayer.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BessinessLogicLayer.Service
{
        public class NotesService : INotesService
        {
            private readonly INotesRepository _repo;
            private readonly IDistributedCache _cache;

        public NotesService(INotesRepository repo, IDistributedCache cache)
            {
                _repo = repo;
               _cache = cache;
        }

        public async Task<Note> CreateAsync(CreateNoteDto dto, int userId)
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

            var createdNote = _repo.Create(note);

            // 🔥 Clear cache
            try
            {
                await _cache.RemoveAsync($"notes_{userId}");
            }
            catch
            {
                // Redis not available – ignore
            }
            return createdNote;
        }
        public async Task<IEnumerable<Note>> GetAllAsync(int userId)
        {
        // 1️⃣ Try Redis first
        var cachedNotes = await _cache.GetStringAsync($"notes_{userId}");

        if (cachedNotes != null)
        {
            return JsonSerializer.Deserialize<List<Note>>(cachedNotes);
        }

        // 2️⃣ Cache miss → DB call
        var notes = _repo.GetAll(userId).ToList();

        // 3️⃣ Store in Redis
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };

        var notesJson = JsonSerializer.Serialize(notes);

        await _cache.SetStringAsync(
            $"notes_{userId}",
            notesJson,
            cacheOptions
        );

        return notes;
    }


        public async Task<Note> UpdateAsync(int noteId, UpdateNoteDto dto, int userId)
        {
            var note = _repo.GetById(noteId, userId);
            if (note == null) throw new Exception("Note not found");

            note.Title = dto.Title;
            note.Description = dto.Description;
            note.Reminder = dto.Reminder;
            note.Colour = dto.Colour;
            note.UpdatedAt = DateTime.UtcNow;

            _repo.Update(note);

            //  Clear cache
            await _cache.RemoveAsync($"notes_{userId}");

            return note;
        }

        public async Task MoveToTrashAsync(int noteId, int userId)
        {
            var note = _repo.GetById(noteId, userId);
            note.IsTrash = true;

            _repo.Update(note);

            //  Clear cache
            await _cache.RemoveAsync($"notes_{userId}");
        }
    }
}

