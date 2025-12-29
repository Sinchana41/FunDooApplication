using BessinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using System.Security.Claims;

namespace FunDooAPP.Controllers
{
    [Authorize] //  JWT mandatory
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _service;

        public NotesController(INotesService service)
        {
            _service = service;
        }

        //  COMMON METHOD: Get UserId from JWT
        private int get_UserId()
        {
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                     ?? User.FindFirst("UserId");

            if (claim == null)
                throw new UnauthorizedAccessException("UserId claim not found in JWT");

            return int.Parse(claim.Value);
        }

        //  CREATE NOTE
        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteDto dto)
        {
            int userId = get_UserId();               //  method CALL
            var note = await _service.CreateAsync(dto, userId); //  await async
            return Created("", note);
        }

        //  GET ALL NOTES (REDIS CACHE)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int userId = get_UserId();
            var notes = await _service.GetAllAsync(userId);
            return Ok(notes);
        }

        //  UPDATE NOTE
        [HttpPut("{noteId}")]
        public async Task<IActionResult> Update(int noteId, UpdateNoteDto dto)
        {
            int userId = get_UserId();
            var note = await _service.UpdateAsync(noteId, dto, userId);
            return Ok(note);
        }

        //  MOVE TO TRASH (SOFT DELETE)
        [HttpDelete("{noteId}")]
        public async Task<IActionResult> MoveToTrash(int noteId)
        {
            int userId = get_UserId();
            await _service.MoveToTrashAsync(noteId, userId);
            return NoContent();
        }
    }
}
