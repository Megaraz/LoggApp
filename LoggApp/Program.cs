using DataAccess;
namespace Presentation

{
    internal class Program
    {

        static async Task Main(string[] args)
        {

            await using var dbContext = new LoggAppContext();

            App app = new App(dbContext);

            app.Init();
            await app.Run();


        }

    }
}
