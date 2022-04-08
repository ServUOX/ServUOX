using Server.Accounting;
using static System.Console;

namespace Server.Misc
{
    public class AccountPrompt
    {
        public static void Initialize()
        {
            if (Accounts.Count == 0 && !Core.Service)
            {
                WriteLine("This server has no accounts.");
                Write("Do you want to create the owner account now? (y/n)");

                string key = ReadLine();

                if (key.ToUpper() == "Y")
                {
                    WriteLine();

                    Write("Username: ");
                    string username = ReadLine();

                    Write("Password: ");
                    string password = ReadLine();

                    Account a = new Account(username, password);
                    a.AccessLevel = AccessLevel.Owner;

                    WriteLine("Account created.");
                }
                else
                {
                    WriteLine();

                    WriteLine("Account not created.");
                }
            }
        }
    }
}
