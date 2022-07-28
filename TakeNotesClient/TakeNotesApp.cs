using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeNotesClient.Models;
using TakeNotesClient.Requesters;
using TakeNotesClient.Services;

namespace TakeNotesClient
{
    public class TakeNotesApp
    {
        private readonly TakeNotesConsoleHelper helper = new TakeNotesConsoleHelper();
        private readonly TakeNotesRequester takeNotesRequester;

        public TakeNotesApp(string apiUrl)
        {
            takeNotesRequester = new TakeNotesRequester(apiUrl);
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                if (takeNotesRequester.IsLoggedIn)
                {
                    keepGoing = RunAuthenticated();
                }
                else
                {
                    keepGoing = RunUnauthenticated();
                }
            }
        }

        private bool RunUnauthenticated()
        {
            helper.PrintLoginMenu();
            int menuSelection = helper.PromptForInteger("Please choose an option", 0, 2);
            while (true)
            {
                if (menuSelection == 0)
                {
                    return false;
                }

                if (menuSelection == 1)
                {
                    Login();
                    return true;
                }

                if (menuSelection == 2)
                {
                    Register();
                    return true; 
                }
                helper.PrintMessage("Invalid selection. Please choose an option.");
                helper.Pause();
            }
        }

        private bool RunAuthenticated()
        {
            helper.PrintMainMenu(takeNotesRequester.UserName);
            int menuSelection = helper.PromptForInteger("Please choose an option", 0, 3);
            if (menuSelection == 0)
            {
                return false;
            }

            if (menuSelection == 1)
            {
                helper.DisplayNotes(takeNotesRequester.GetUserId(), takeNotesRequester.GetNotes());
                helper.Pause();
            }

            if (menuSelection == 2)
            {
                Note note = new Note();
                note.UserId = takeNotesRequester.GetUserId();
                Console.WriteLine("Enter the title of your new note.");
                note.Title = Console.ReadLine();
                Console.WriteLine("Now enter your new note!");
                note.NoteContent = Console.ReadLine();
                takeNotesRequester.AddNote(note);
                helper.Pause();
            }

            if (menuSelection == 3)
            {
                takeNotesRequester.Logout();
                helper.PrintMessage("You are now logged out");
            }

            return true;
        }

        private void Login()
        {
            NewUser loginUser = helper.PromptForLogin();
            if (loginUser == null)
            {
                return;
            }

            try
            {
                RegisteredUser user = takeNotesRequester.Login(loginUser);
                if (user == null)
                {
                    helper.PrintMessage("Login failed.");
                }
                else
                {
                    helper.PrintMessage("You are now logged in");
                }
            }
            catch (Exception)
            {
                helper.PrintMessage("Login failed.");
            }
            helper.Pause();
        }

        private void Register()
        {
            NewUser registerUser = helper.PromptForLogin();
            if (registerUser == null)
            {
                return;
            }
            try
            {
                bool isRegistered = takeNotesRequester.Register(registerUser);
                if (isRegistered)
                {
                    helper.PrintMessage("Registration was successful. Please log in.");
                }
                else
                {
                    helper.PrintMessage("Registration was unsuccessful.");
                }
            }
            catch (Exception)
            {
                helper.PrintMessage("Registration was unsuccessful.");
            }
            helper.Pause();
        }
    }
}
