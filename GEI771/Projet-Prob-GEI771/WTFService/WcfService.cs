using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Security.Permissions;
using log4net;
using System.IO;
using System.Runtime.Serialization;
using Definitions;
using Sondage;

namespace WcfService
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class WcfSvc
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WcfSvc));

        /// <summary>
        /// Obtenirs the sondages.
        /// </summary>
        /// <returns></returns>
        [WebGet(UriTemplate = "GetAvailablePolls")]
        public IList<Poll> GetAvailablePolls()
        {
            log.Info("GetAvailablePolls");
            return SimpleSondageDAO.Instance.GetAvailablePolls();
        }


        /// <summary>
        /// Gets the next question.
        /// </summary>
        /// <param name="pollID">The poll identifier.</param>
        /// <param name="currentQuestionID">The current question identifier.</param>
        /// <returns></returns>
        [WebGet(UriTemplate = "GetNextQuestion/{pollID}/{currentQuestionID}")]
        public PollQuestion GetNextQuestion(string pollID, string currentQuestionID)
        {
            log.Info("GetNextQuestion");
            int ipollID = -1, icurrentQuestionID = -1;
            if (Int32.TryParse(currentQuestionID, out  icurrentQuestionID) &&
                Int32.TryParse(pollID, out  ipollID))
            {
                return SimpleSondageDAO.Instance.GetNextQuestion(ipollID, icurrentQuestionID);
            }
            return null;
        }

        /// <summary>
        /// Saves the answer.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="pollID">The poll identifier.</param>
        /// <param name="questionID">The question identifier.</param>
        /// <param name="text">The text.</param>
        [WebGet(UriTemplate = "SaveAnswer/{userID}/{pollID}/{questionID}/{text}")]
        public void SaveAnswer(string userID, string pollID, string questionID, string text)
        {
            log.Info("SaveAnswer");
            int iuserID = -1, ipollID = -1 , iquestionID = -1;
            if (Int32.TryParse(userID, out  iuserID) && 
                Int32.TryParse(pollID, out  ipollID) &&
                Int32.TryParse(questionID, out  iquestionID)) 
            {
                SimpleSondageDAO.Instance.SaveAnswer(
                    iuserID, 
                    new PollQuestion(){ PollId = ipollID , QuestionId = iquestionID, Text = text }
                );
            }
        }
    }

}
