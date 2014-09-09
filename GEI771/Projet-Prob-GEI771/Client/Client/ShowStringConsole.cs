using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    static class ShowStringConsole
    {
        /// <summary>
        /// Names the application.
        /// </summary>
        public static void NameApplication()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Sondages");
            Console.WriteLine("***********************************************************");
        }

        /// <summary>
        /// Generals the menu.
        /// </summary>
        public static void GeneralMenu()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Choisir une des options ci-dessous");
            Console.WriteLine("1: Afficher les sondages disponibles");
            Console.WriteLine("2: Repondre à un sondage");
            Console.WriteLine("Escape : Pour quitter l'application");
            Console.WriteLine("***********************************************************");
        }

        /// <summary>
        /// Polls the menu.
        /// </summary>
        public static void PollMenu()
        {
            Console.WriteLine("Inserer le numero de sondage que vous voulez repondre");
        }

        /// <summary>
        /// Answers the poll menu.
        /// </summary>
        public static void AnswerPollMenu()
        {
            Console.WriteLine("Reponse de l'utilisateur");
            Console.WriteLine("Inscrire votre reponse");
        }

        /// <summary>
        /// Invalids the command.
        /// </summary>
        public static void InvalidCommand()
        {
            Console.WriteLine("Commande invalide");
        }

        /// <summary>
        /// Authentifications the menu.
        /// </summary>
        public static void AuthentificationMenu()
        {
            Console.WriteLine("Entrer les informations necessaires pour votre authentification");
            Console.WriteLine("Votre numero d'usager (seulement des chiffres)");
        }

        /// <summary>
        /// Passwords the menu.
        /// </summary>
        public static void PasswordMenu()
        {
            Console.WriteLine("Votre mot de passe");
        }

        /// <summary>
        /// Invalids the username.
        /// </summary>
        public static void InvalidUsername()
        {
            Console.WriteLine("Votre userID ne respecte pas le format numerique");
        }

        /// <summary>
        /// Invalids the password.
        /// </summary>
        public static void InvalidPassword()
        {
            Console.WriteLine("Votre mot de passe est invalide");
        }
    }
}
