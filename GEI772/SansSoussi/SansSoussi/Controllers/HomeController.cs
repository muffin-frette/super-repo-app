using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;

namespace SansSoussi.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Parce que marcher devrait se faire SansSoussi";

            return View();
        }

        public ActionResult Comments()
        {
            List<string> comments = new List<string>();

            //Get current user from default membership provider
            MembershipUser user = Membership.Provider.GetUser(HttpContext.User.Identity.Name, true);
            if (user != null)
            {
                String strCmdSelectComment = "SELECT Comment FROM Comments WHERE UserId = @UserId";
                
                using (SqlConnection connection = GetConnection())
                using (SqlCommand command = new SqlCommand(strCmdSelectComment, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.ProviderUserKey);
                    connection.Open();

                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            comments.Add(rd.GetString(0));
                        }
                    }
                }
            }
            return View(comments);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comments(string comment)
        {
            string status = "success";
            try
            {
                //Get current user from default membership provider
                MembershipUser user = Membership.Provider.GetUser(HttpContext.User.Identity.Name, true);
                if (user != null)
                {
                    String strCmdInsertComment = "INSERT INTO Comments(UserId, CommentId, Comment) VALUES(@UserId, @CommentId, @Comment)";
                    using(SqlConnection connection = GetConnection())
                    using (SqlCommand command = new SqlCommand(strCmdInsertComment, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user.ProviderUserKey);
                        command.Parameters.AddWithValue("@CommentId", Guid.NewGuid());
                        command.Parameters.AddWithValue("@Comment", HttpUtility.HtmlEncode(comment));
                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    throw new Exception("Vous devez vous connecter");
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return Json(status);
        }

        public ActionResult Search(string searchData)
        {
            List<string> searchResults = new List<string>();

            //Get current user from default membership provider
            MembershipUser user = Membership.Provider.GetUser(HttpContext.User.Identity.Name, true);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(searchData))
                {
                    String strCmdSelectSearchComment = "SELECT Comment FROM Comments WHERE UserId = @UserId AND Comment LIKE @SearchData";
                    using (SqlConnection connection = GetConnection())
                    using (SqlCommand command = new SqlCommand(strCmdSelectSearchComment, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user.ProviderUserKey);
                        command.Parameters.AddWithValue("@SearchData", "%" + searchData +"%");
                        connection.Open();
                        using (SqlDataReader rd = command.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                searchResults.Add(rd.GetString(0));
                            }
                        }
                    }
                }
            }
            return View(searchResults);
        }

        [HttpGet]
        public ActionResult Emails()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Emails(object form)
        {            
            List<string> searchResults = new List<string>();

            HttpCookie cookie = HttpContext.Request.Cookies["username"];
            
            List<string> cookieString = new List<string>();

            //Decode the cookie from base64 encoding
            //TODO DECRYPT HERE
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(cookie.Value);
            string strCookieValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            //get user role base on cookie value
            string[] roles = Roles.GetRolesForUser(strCookieValue);

            bool isAdmin = roles.Contains("admin");

            if (isAdmin)
            {
                String strCmdSelectEmail = "SELECT Email FROM aspnet_Membership";
                using (SqlConnection connection = GetConnection())
                using (SqlCommand command = new SqlCommand(strCmdSelectEmail, connection))
                {
                    connection.Open();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            searchResults.Add(rd.GetString(0));
                        }
                    }
                }
            }


            return Json(searchResults);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
