using AUEDashboard.Data.Models.Domain;
using AUEDashboard.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AUEDashboard.UI.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IUserRepository _userRepository;
        private  readonly IAssignmentRepository _assignmentRepository;

        public FacultyController(IUserRepository userRepository,IAssignmentRepository assignmentRepository)
        {
            _userRepository = userRepository;
            _assignmentRepository = assignmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Dashboard()
        {
            // Get Student Listing
            var students = await _userRepository.GetAllByRole("Student");
            ViewBag.Students = students; // Or pass via a comprehensive Dashboard ViewModel

            // Get Assignment Listing
            var facultyId= HttpContext.Session.GetString("UserId");

            var assignments = await _assignmentRepository.GetAllAssignmentsByFacultyId (facultyId);
            ViewBag.Assignments = assignments;
            // ... other data for dashboard
            TempData["Controllername"] = "Faculty";

            return View();
        }
        [HttpGet]
        public IActionResult CreateAssignment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAssignment(AssignmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filePath = null;
                string fileName = null;

                if (model.AssignmentFile != null && model.AssignmentFile.Length > 0)
                {
                    // Ensure the uploads directory exists
                   // string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "assignments");
                    //Directory.CreateDirectory(uploadsFolder); // Create if it doesn't exist

                    //fileName = Path.GetFileName(model.AssignmentFile.FileName);
                    // Create a unique file name to avoid conflicts
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    //filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.AssignmentFile.CopyToAsync(stream);
                    }
                    filePath = "/uploads/assignments/" + uniqueFileName; // Store relative path
                }
                var userId= HttpContext.Session.GetString("UserId");
                var assignment = new Assignment
                {
                    //FacultyUserId = User.FindFirstValue(ClaimTypes.NameIdentifier), // Get current faculty user ID
                   // FacultyUserId ="1", //Hard coded
                    FacultyUserId =userId,
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    MaxMarks = model.MaxMarks,
                    FilePath = filePath,
                    FileName = fileName
                };

                await _assignmentRepository.CreateAssignmentAsync(assignment);
                TempData["SuccessMessage"] = "Assignment created successfully!";
                return RedirectToAction("Dashboard");
            }
            return View(model);
        }
    }
}
