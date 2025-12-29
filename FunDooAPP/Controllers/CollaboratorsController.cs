using BessinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using System.Security.Claims;

namespace FunDooAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorsController : ControllerBase
    {
        private readonly ICollaboratorService _service;

        public CollaboratorsController(ICollaboratorService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                        ?? User.FindFirst("UserId");

            if (claim == null)
                throw new UnauthorizedAccessException();

            return int.Parse(claim.Value);
        }

        // POST /api/collaborators
        [HttpPost]
        public IActionResult Add(AddCollaboratorDto dto)
        {
            var result = _service.Add(dto, GetUserId());
            return Ok(result);
        }

        // GET /api/collaborators/{noteId}
        [HttpGet("{noteId}")]
        public IActionResult GetByNote(int noteId)
        {
            return Ok(_service.GetByNoteId(noteId, GetUserId()));
        }

        // DELETE /api/collaborators/{collaboratorId}
        [HttpDelete("{collaboratorId}")]
        public IActionResult Delete(int collaboratorId)
        {
            _service.Delete(collaboratorId, GetUserId());
            return NoContent();
        }

        // DELETE /api/collaborators/{noteId}/{email}
        [HttpDelete("{noteId}/{email}")]
        public IActionResult DeleteByEmail(int noteId, string email)
        {
            _service.DeleteByEmail(noteId, email, GetUserId());
            return NoContent();
        }

        // GET /api/collaborators/shared-notes
        [HttpGet("shared-notes")]
        public IActionResult GetSharedNotes()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(_service.GetSharedNotes(email));
        }
    }
}
