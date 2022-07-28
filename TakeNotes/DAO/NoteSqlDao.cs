using System.Data.SqlClient;
using TakeNotesServer.Models;

namespace TakeNotesServer.DAO
{
    public class NoteSqlDao : INoteDao
    {
        private readonly string connectionString;

        public NoteSqlDao(string dbconnectionString)
        {
            connectionString = dbconnectionString;
        }

        public List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from note_table;", conn);
                //cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Note note = new Note();
                    note.Id = Convert.ToInt32(reader["note_id"]);
                    note.Title = Convert.ToString(reader["note_title"]);
                    note.NoteContent = Convert.ToString(reader["note_content"]);
                    notes.Add(note);
                }
            }
            return notes;
        }

        public Note AddNote(Note note)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO note_table (note_title, note_content, user_id) OUTPUT INSERTED.note_id VALUES(@note_title, @note_content, @user_id)", conn);
                cmd.Parameters.AddWithValue("@note_content", note.NoteContent);
                cmd.Parameters.AddWithValue("@note_title", note.Title);
                cmd.Parameters.AddWithValue("@user_id", note.UserId);
                note.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return note;
        }
    }
}
