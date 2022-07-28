using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeNotesClient.Models;

namespace TakeNotesClient.Services
{
    public class TakeNotesConsoleHelper
    {
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TakeNotes! Your personal note application.");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your notes");
            Console.WriteLine("2: Add a new note");
            Console.WriteLine("3: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }
        public NewUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            NewUser loginUser = new NewUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        public int PromptForInteger(string message, int minimum, int maximum)
        {
            while (true)
            {
                Console.Write($"{message}");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int selection) && selection >= minimum && selection <= maximum)
                {
                    return selection;
                }
                PrintMessage($"Number is out of range, please try again.");
            }
        }
        public string PromptForString(string message)
        {
            Console.Write($"{message}");
            string input = Console.ReadLine();
            return input;
        }
        public string PromptForHiddenString(string message)
        {
            string pass = "";
            Console.Write($"{message}: ");

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Pause(string message = null)
        {
            if (message == null)
            {
                message = "Press any key to continue:";
            }
            Console.Write(message);
            Console.ReadKey();
        }

        public bool DisplayNotes(int userId, List<Note> notes)
        {
            if (notes.Count == 0)
            {
                Console.WriteLine("No notes saved!");
                return false;
            }

            Console.WriteLine("Title      -      Content");
            foreach (Note note in notes)
            {
                    Console.WriteLine($"{note.Title}   -  {note.NoteContent}");
            }
            return true;
        }
       
    }
}
