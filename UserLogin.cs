using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    
    internal class UserLogin
        {
        
        private string name, password;

            private HashSet<(string, string)> uniqueStorage = new HashSet<(string, string)>() { ("Test", "123") };

            public HashSet<(string, string)> UniqueStorage
            {
                get { return uniqueStorage; }//Read
                set { uniqueStorage = value ?? new HashSet<(string, string)>(); }//Write
            }

            private void writeMessage()
            {
                Console.CursorVisible = true;
                Console.ResetColor();
                Console.WriteLine(@"
                    ██       ██████   ██████  ██ ███    ██     ██████   █████   ██████  ███████ 
                    ██      ██    ██ ██       ██ ████   ██     ██   ██ ██   ██ ██       ██      
                    ██      ██    ██ ██   ███ ██ ██ ██  ██     ██████  ███████ ██   ███ █████   
                    ██      ██    ██ ██    ██ ██ ██  ██ ██     ██      ██   ██ ██    ██ ██      
                    ███████  ██████   ██████  ██ ██   ████     ██      ██   ██  ██████  ███████ 
                                                                                                                                       
                ");

                Console.SetCursorPosition(50, 9);
                Console.WriteLine("Enter AccountName:");
                Console.SetCursorPosition(50, 10);
                Console.WriteLine("Enter Password:");
            }

        #region displayLogin
        public void displayLogin()
            {
                bool found = false;
                int i = 3;
                do
                {
                    Thread thread = new Thread(writeMessage);
                    thread.Start();
                    thread.Join();
                    Console.SetCursorPosition(68, 9);
                    name = Console.ReadLine();
                    Console.SetCursorPosition(65, 10);
                    password = ReadPassword();

                    foreach (var store in UniqueStorage)
                    {
                        if (store.Item1 == name && store.Item2 == password)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found == true)
                    {
                        Console.WriteLine("You have logged in");
                        break;
                    }
                    else
                    {
                        i--;
                        Console.Clear();
                        Console.WriteLine("Password or username is incorrect please try again");
                        Console.WriteLine($"Attempts remaining: {i}");
                    }
                    if (i == 0)
                    {
                        Console.WriteLine("Too many attempts please try again later!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("Shutting down...");
                        System.Threading.Thread.Sleep(1000);
                        Environment.Exit(0);
                    }
                }
                while (found == false);
                
        }
        #endregion

        #region ReadPassword
        public static string ReadPassword()
        {
            var pwd = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && pwd.Length > 0)
                {
                    pwd.Remove(pwd.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pwd.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return pwd.ToString();
        }
        #endregion
    }//End of UserLogin Class
}//End of namespace

