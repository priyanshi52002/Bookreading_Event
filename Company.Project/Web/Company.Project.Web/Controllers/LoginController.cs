using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.EventDomain.AppServices.DTOs;
using Company.Project.EventDomain.Repository;
using Company.Project.Web.Mapper;
using Company.Project.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.Web.Login
{
    //controller manage all the action related to login
    public class LoginController : Controller
    {
        private IEventRepository _eventRepository;

        public LoginController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        //Action method for view Login page
        public IActionResult Login()
        {
            return View();
        }
        
        //Login post method call when user Login
        [HttpPost]
        [ActionName("Login")]
        public IActionResult LoginPost(UserModel loginUserModel)
        {
            //Map user model data to user class
            User user = new LoginUserModelToUserMapper().LoginUserToUserMapping(loginUserModel);

            //If user is valid then add it to database
            if (_eventRepository.IsValidUser(user))
            {
                DbWorks addUserToDb = new DbWorks(_eventRepository);
                User userWithFullDetails = addUserToDb.GetUser(user);

                //Setting session for user use username
                HttpContext.Session.SetString("Username", userWithFullDetails.UserName);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid EmailId or Password";
                return View();
            }
            
        }

        //Logout action method remove session
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }


    }
}
