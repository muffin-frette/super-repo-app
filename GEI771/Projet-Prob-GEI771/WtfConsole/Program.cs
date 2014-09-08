using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Reflection;
using System.IO;
using WcfService;
using System.Web.Routing;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Security.Cryptography;
using System.ServiceModel.Channels;

namespace WtfConsole
{
    class Program
    {
        static void Main()
        {
            var host = new ServiceHost(typeof(WcfSvc));
            host.Open();

            Console.WriteLine("The service is ready at {0}", host.Description.Endpoints[0].ListenUri);
            Console.WriteLine("Press <Enter> to stop the service.");
            Console.ReadLine();

            //Close the ServiceHost.
            host.Close();
            
        }

    }
}
