using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using Definitions;

namespace Client
{
    class Program
    {
        private static bool authoritySelfSigned = true;
        private const string serverString = "https://localhost:8080";

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        static void Main()
        {
            if (authoritySelfSigned)
                ServicePointManager.ServerCertificateValidationCallback += SecurityUtils.CustomXertificateValidation;

            ConsoleProgram();     
        }

        /// <summary>
        /// Consoles the program.
        /// </summary>
        public static void ConsoleProgram()
        {
            bool quit = false;
            string userID = String.Empty, password = String.Empty;
            ShowStringConsole.NameApplication();

            if (!AuthentificationProgram(ref userID, ref password))
            {
                Console.ReadKey();
                return;
            }

            ConsoleKeyInfo cki;
            do
            {    
                ShowStringConsole.GeneralMenu();
                cki = Console.ReadKey();
                Console.WriteLine(string.Empty);
                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        ShowAvailablePolls(userID,password);
                        break;

                    case ConsoleKey.D2:
                        QuestionsProgram(userID,password);
                        break;

                    case ConsoleKey.Escape:
                        quit = true;
                        break;

                    default:
                        ShowStringConsole.InvalidCommand();
                        break;
                }
                Console.Beep();
            }
            while (quit == false);
        }

        /// <summary>
        /// Authentifications the program.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private static bool AuthentificationProgram(ref string userID, ref string password)
        {
            ShowStringConsole.AuthentificationMenu();
            userID = Console.ReadLine();
            int iUserID = 0;
            if (!Int32.TryParse(userID, out iUserID))
            {
                ShowStringConsole.InvalidUsername();
                return false;
            }

            ShowStringConsole.PasswordMenu();
            password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                ShowStringConsole.InvalidPassword();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Questionses the program.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        private static void QuestionsProgram(string userID, string password)
        {
            ShowStringConsole.PollMenu();
            string pollId = Console.ReadLine();
            int iPollID = -1;
            if (Int32.TryParse(pollId, out  iPollID))
            {
                int currentQuestion = -1;
                bool terminated = false;

                //Itérer sur toutes les questions
                while (terminated == false)
                {
                    var question = RestSerialization.GetNextQuestion(userID, password,serverString, iPollID, currentQuestion);
                    if (question == null)
                    {
                        terminated = true;
                        continue;
                    }

                    Console.WriteLine(question);
                    ShowStringConsole.AnswerPollMenu();
                    string answer = Console.ReadLine();
                    RestSerialization.SaveAnswer(
                        userID,
                        password,
                        serverString,
                        new PollQuestion()
                        {
                            PollId = iPollID,
                            QuestionId = question.QuestionId,
                            Text = answer
                        }
                    );

                    //Continuer sur la prochaine question
                    currentQuestion = question.QuestionId;
                }
            }
        }

        /// <summary>
        /// Shows the available polls.
        /// </summary>
        private static void ShowAvailablePolls(string userID, string password)
        {
            var allPolls = RestSerialization.GetAvailablePolls(userID, password, serverString);
            if (allPolls != null)
              foreach (var poll in allPolls)
                 Console.WriteLine(poll);
        }
    }
}
