using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using log4net;

namespace WtfService
{

    /// <summary>
    /// Here is where we actually validate the password
    /// </summary>
    class WtfUserNamePasswordValidator : UserNamePasswordValidator
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WtfUserNamePasswordValidator));

        public override void Validate(string userID, string password)
        {
            int iUserID;
            if ( (!Int32.TryParse(userID, out  iUserID)) || string.IsNullOrEmpty(password))
                 throw new FaultException("Invalid username or(and password");

            log.Info("Username: " + "[" + userID + "], state connection: " + " [connected]");
        }

    }

}
