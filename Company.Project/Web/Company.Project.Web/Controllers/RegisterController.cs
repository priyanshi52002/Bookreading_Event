using Company.Project.EventDomain.Appservices.Domain;
using Microsoft.AspNetCore.Mvc;
using Company.Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Project.Web.Mapper;
using Company.Project.EventDomain.Repository;
using Company.Project.EventDomain.AppServices.DTOs;

namespace Company.Project.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IEventRepository _EventRepository;

        public RegisterController(IEventRepository EventRepository)
        {
            _EventRepository = EventRepository;
        }

        //SignUp return signup View()
        public IActionResult SignUp()
        {
            return View();
        }


        //SignUp post request method
        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult RegisterPost(RegisterUserModel RegisteruserModel)
        {  

            User user = new RegisterUserModelToUserMapper().RegisterUserToUserMapping(RegisteruserModel);
            if (ModelState.IsValid)
            {
                //Validate user and add it to database
                DbWorks addUserToDb = new DbWorks(_EventRepository);
                if(addUserToDb.IfValidUserThanAdd(user))
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    ViewBag.Message = "User Already Registered";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
