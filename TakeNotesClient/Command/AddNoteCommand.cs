using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TakeNotesClient.Models;

namespace TakeNotesClient.Command
{
    public class AddNoteCommand : Command
    {
        public static RestClient client = null;
        private Note noteToAdd = new Note();

        public AddNoteCommand(string apiUrl)
        {
            client = new RestClient(apiUrl);
        }

        public void Execute()
        {
            AddNote(noteToAdd);
        }


        public bool AddNote(Note note)
        {
            var request = new RestRequest("note").AddJsonBody(note);
            var response = client.Post<Note>(request);
            if (response != null)
            {
                return true;
            }
            return false;
        }

        public void NoteToAdd(Note note)
        {
            noteToAdd = note;
            Execute();
        }
    }
}
