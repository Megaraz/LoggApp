using AppLogic;

namespace Presentation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize dbContext here only once and pass it all the way
            await using var dbContext = new LoggAppContext();

            // Create the App instance with the dbContext
            App app = new App(dbContext);


            // Lite för sent nu har jag insett att mycket av mina grejer så som Menuhandlers och app bör ligga i Applogic,
            // men jag hann inte refaktorera det innan deadline.
            // 
            // I övrigt så är jag rätt nöjd med koden, jag har fokuserat rätt mycket på att göra det modulärt/utbyggbart och testbart,
            // det blev väldigt mycket större än jag hade tänkt dock, vilket gjorde att en del grejer blev 
            // mindre snygga/framstressade nu på slutet.
            // Speciellt UI och konsol-appen i sig blev väl inte jättebra, men jag har fått till i princip all funktionalitet nu.
            // Man kan göra en hel del mer nu än vid presentationen i all fall!

            // Det är möjligtvis några models/entities som inte är implementerade,
            // Så som Supplement(s), Medication, Food osv. Men de kallas inte på någonstans så ska inte påverka.


            // Jag valde att dela upp initieringen i separata metoder för att hålla koden ren och lättläst,
            // istället för att initiera direkt i konstruktorn t.ex.

            // Initialize all components
            app.InitRepos();
            app.InitServices();
            app.InitControllers();
            app.InitPresentation();

            // Run the application
            await app.Run();

        }
    }
}
