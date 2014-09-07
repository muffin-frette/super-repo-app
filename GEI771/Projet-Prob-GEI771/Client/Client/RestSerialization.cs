using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Definitions;

namespace Client
{
    /// <summary>
    /// 
    /// </summary>
    static class RestSerialization
    {

        /// <summary>
        /// Saves the answer.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="serverString">The server string.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="question">The question.</param>
        public static void SaveAnswer(string userID, string password,string serverString, PollQuestion question)
        {
            if (question == null)
                return;

            RestRequest.GetRestConnection(
                userID,
                password,
                serverString,
                "WtfService/SaveAnswer/" +
                Int32.Parse(userID, new CultureInfo("fr-FR")) + "/" +
                question.PollId + "/" +
                question.QuestionId + "/" +
                question.Text
            );
        }

        /// <summary>
        /// Gets the next question.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="serverString">The server string.</param>
        /// <param name="pollId">The poll identifier.</param>
        /// <param name="currentQuestionId">The current question identifier.</param>
        /// <returns></returns>
        public static PollQuestion GetNextQuestion(string userID, string password,string serverString,int pollId, int currentQuestionId)
        {
            HttpContent content = RestRequest.GetRestConnection(
                userID, 
                password, 
                serverString, 
                "WtfService/GetNextQuestion/" + pollId + "/" + currentQuestionId
            );
            if (content == null)
             return null;

            DataContractJsonSerializer serializerToUpload = new DataContractJsonSerializer(typeof(PollQuestion));
            Stream stream = content.ReadAsStreamAsync().Result;
            if (stream.Length == 0)
                return null;

            return serializerToUpload.ReadObject(stream) as PollQuestion;
        }

        /// <summary>
        /// Gets the available polls.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="serverString">The server string.</param>
        /// <returns></returns>
        public static IList<Poll> GetAvailablePolls(string userID, string password,string serverString)
        {
            HttpContent content = RestRequest.GetRestConnection(userID,password,serverString, "WtfService/GetAvailablePolls");
            if (content == null)
                return null;

            DataContractJsonSerializer serializerToUpload = new DataContractJsonSerializer(typeof(IList<Poll>));
            Stream stream = content.ReadAsStreamAsync().Result;
            if (stream.Length == 0)
                return null;

            return serializerToUpload.ReadObject(stream) as IList<Poll>;
        }
    }
}
