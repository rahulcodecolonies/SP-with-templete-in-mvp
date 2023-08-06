using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SP.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SP.Controllers
{
    public class AccountController : Controller
    {

        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }
     
        [HttpPost]
        public IActionResult Login(UserLogin m)
        {
           
            SqlConnection cons = new SqlConnection(_configuration.GetConnectionString("Reg"));
            cons.Open();
            SqlCommand com = new SqlCommand("Emplogin", cons);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Email", m.Email);   //for username 
            com.Parameters.AddWithValue("@Password", m.Password);  //for word

            int usercount = (Int32)com.ExecuteScalar();// for taking single value

            if (usercount == 1)  // comparing users from table 
            {
                HttpContext.Session.SetString("Email", m.Email);
                return RedirectToAction("Welcome", "Account"); 
            }
            else
            {
                
                return RedirectToAction("errorLogin", "Account");
            }
        }
        [SessionCheck]
        public IActionResult Welcome()
        {
            ViewBag.Message = "success";
            ViewBag.Message2 = HttpContext.Session.GetString("Email");
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
        }
        public IActionResult Logout()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }



    }
}
