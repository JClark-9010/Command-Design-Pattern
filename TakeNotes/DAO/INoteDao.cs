using TakeNotesServer.Models;

namespace TakeNotesServer.DAO
{
    public interface INoteDao
    {
        List<Note> GetNotes();
        Note AddNote(Note note);
    }
}
