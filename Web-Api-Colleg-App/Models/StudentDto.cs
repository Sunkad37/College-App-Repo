using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Api_Colleg_App.Models
{
	public class StudentDto
	{
        [Required]
        public int StudentId { get; set; }
        [Required]
        public required string StudentName { get; set; }
        [Required]
        public required string Address { get; set; }
        [EmailAddress]
        public required string Emial { get; set; }
	}
}

