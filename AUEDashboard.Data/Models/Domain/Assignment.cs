using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUEDashboard.Data.Models.Domain
{
    public class AssignmentCreateViewModel
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        [Required]
        [Range(1, 1000)] // Example range for max marks
        public int MaxMarks { get; set; }
        
        public IFormFile? AssignmentFile { get; set; } // For file upload
    }

    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string FacultyUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxMarks { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
