namespace TakeNotesClient
{
    internal class Program
    {
        private const string apiUrl = "https://localhost:44315/";
        static void Main(string[] args)
        {
            TakeNotesApp app = new TakeNotesApp(apiUrl);
            app.Run();
        }
    }
}