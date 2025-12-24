using BessinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;

namespace FunDooAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
            private readonly INotesService _service;

            public NotesController(INotesService service)
            {
                _service = service;
            }

            private int UserId =>
                int.Parse(User.FindFirst("UserId").Value);

            // Create Note
            [HttpPost]
            public IActionResult Create(CreateNoteDto dto)
            {
                var note = _service.Create(dto, UserId);
                return Created("", note);
            }

            //Get All Notes
            [HttpGet]
            public IActionResult GetAll()
            {
                return Ok(_service.GetAll(UserId));
            }

            //Update Note
            [HttpPut("{noteId}")]
            public IActionResult Update(int noteId, UpdateNoteDto dto)
            {
                return Ok(_service.Update(noteId, dto, UserId));
            }

            //Move to Trash
            [HttpDelete("{noteId}")]
            public IActionResult Trash(int noteId)
            {
                _service.MoveToTrash(noteId, UserId);
                return NoContent();
            }
    }

}

