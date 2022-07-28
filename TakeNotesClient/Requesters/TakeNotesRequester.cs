using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using TakeNotesClient.Models;
using TakeNotesClient.Command;

namespace TakeNotesClient.Requesters
{
    public class TakeNotesRequester : LoginRequester
    {
        private AddNoteCommand addNoteCommand;
        private GetNoteCommand getNoteCommand;

        public TakeNotesRequester(string apiUrl) : base(apiUrl)
        {
            getNoteCommand = new GetNoteCommand(apiUrl);
            addNoteCommand = new AddNoteCommand(apiUrl);
        }

        public List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            notes = getNoteCommand.GetNotes();
            return notes;
        }

        public void AddNote(Note note)
        {
            Note noteToAdd = note;
            addNoteCommand.NoteToAdd(noteToAdd);
        }

        public int GetUserId()
        
        {
            return UserId;
        }
    }
}
