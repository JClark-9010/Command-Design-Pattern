using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeNotesServer.DAO;
using TakeNotesServer.Models;

namespace TakeNotesServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteDao noteDao;
        private readonly User user;

        public NoteController(INoteDao noteDao)
        {
            this.noteDao = noteDao;
            
        }

        [HttpGet]
        public ActionResult<List<Note>> GetNotes()
        {
            List<Note> notes = noteDao.GetNotes();
            return Ok(notes);
        }

        [HttpPost]
        public ActionResult<Note> AddNote(Note note)
        {
            Note result = noteDao.AddNote(note);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
