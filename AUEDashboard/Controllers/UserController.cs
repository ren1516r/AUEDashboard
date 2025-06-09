using AUEDashboard.Data.Models.Domain;
using AUEDashboard.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AUEDashboard.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View("View");
        }
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(user);
                bool addPersonResult = await _userRepository.AddAsync(user);
                if (addPersonResult)
                    TempData["msg"] = "Successfully added";
                else
                    TempData["msg"] = "Could not added";
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not added";
            }
            return RedirectToAction(nameof(Add));
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
           var res =  await _userRepository.GetByUserId(user.Username);
            if(res == null)
            {
                TempData["Message"] = "User not found";

                return View("View");
            }
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("UserId", res.Id.ToString());
            if (res.Role == "Faculty")
                return RedirectToAction("Dashboard","Faculty");
            else if (res.Role == "Student")
                return RedirectToAction("Dashboard", "Student");
            else
                return View();            
        }
        public async Task<IActionResult> DisplayAll()
        {
            var people = await _userRepository.GetAllAsync();
            return View(people);
        }

    }
}
