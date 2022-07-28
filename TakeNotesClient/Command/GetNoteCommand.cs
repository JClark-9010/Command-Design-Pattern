using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TakeNotesClient.Models;
using TakeNotesClient.Requesters;

namespace TakeNotesClient.Command
{
    public class GetNoteCommand : Command
    {
        public static RestClient client;

        public GetNoteCommand(string apiUrl)
        {
            client = new RestClient(apiUrl);
        }

        public void Execute()
        {
            GetNotes();
        }

        public List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            var request = new RestRequest("note");
            var response = client.Get<List<Note>>(request);
            if (response != null)
            {
                foreach (Note note in response)
                {
                    notes.Add(note);
                }
            }
            return notes;
        }
    }
}
