using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    static class RestRequest
    {

        /// <summary>
        /// Gets the rest connection.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="serverString">The server string.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static HttpContent GetRestConnection(string userID, string password, string serverString, string url)
        {
            var handler = new WebRequestHandler();
            handler.Credentials = new NetworkCredential(userID, password);
            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverString);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content;
                    }
                }
                catch (HttpRequestException)
                {
                    throw;
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine(ex.InnerException.InnerException.Message + "\n");
                }
                catch (WebException)
                {
                    throw;
                }
            }
            return null;
        }
    }
}
