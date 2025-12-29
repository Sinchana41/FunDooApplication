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
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _service;

        public LabelsController(ILabelService service)
        {
            _service = service;
        }
        // Helper: Get UserId from JWT
        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                        ?? User.FindFirst("UserId");

            if (claim == null)
                throw new UnauthorizedAccessException("UserId not found in token");

            return int.Parse(claim.Value);
        }


        // Create Label
        [HttpPost]
        public IActionResult Create(CreateLabelDto dto)
        {
            var result = _service.Create(dto, GetUserId());
            return Ok(result);
        }

        // Get All Labels
        [HttpGet]
        public IActionResult GetAll()
        {
            var labels = _service.GetAll(GetUserId());
            return Ok(labels);
        }

        // Update Label
        [HttpPut("{labelId}")]
        public IActionResult Update(int labelId, UpdateLabelDto dto)
        {
            var result = _service.Update(labelId, dto, GetUserId());
            return Ok(result);
        }

        // Delete Label
        [HttpDelete("{labelId}")]
        public IActionResult Delete(int labelId)
        {
            _service.Delete(labelId, GetUserId());
            return NoContent();
        }


        // Add Label to Note
        [HttpPost("{labelId}/notes/{noteId}")]
        public IActionResult AddLabelToNote(int labelId, int noteId)
        {
            _service.AddLabelToNote(labelId, noteId, GetUserId());
            return Ok(new { message = "Label added to note" });
        }


        // Remove Label from Note
        [HttpDelete("{labelId}/notes/{noteId}")]
        public IActionResult RemoveLabelFromNote(int labelId, int noteId)
        {
            _service.RemoveLabelFromNote(labelId, noteId, GetUserId());
            return Ok(new { message = "Label removed from note" });
        }

        // Get Notes by Label
        [HttpGet("{labelId}/notes")]
        public IActionResult GetNotesByLabel(int labelId)
        {
            var notes = _service.GetNotesByLabel(labelId, GetUserId());
            return Ok(notes);
        }
    }
}
