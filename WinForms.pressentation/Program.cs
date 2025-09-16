namespace WinForms.pressentation
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
<<<<<<< HEAD
            Application.Run(new CartForm());
=======
            Application.Run(new AdminForm(null));
>>>>>>> 8eb9597a3720c8c9881c5e005aeebf1ccd7c8ce7
        }
    }
}