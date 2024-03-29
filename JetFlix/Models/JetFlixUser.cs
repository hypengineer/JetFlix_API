using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace JetFlix_API.Models
{
	public class JetFlixUser:IdentityUser<long>
	{
		[Column(TypeName ="date")]
		public DateTime BirthDate { get; set; }

		[Column(TypeName ="nvarchar(100)")]
        [StringLength(100,MinimumLength =2)]
		public string Name { get; set; } = "";

		public bool Passive { get; set; }

		[NotMapped] //Databasede gösterilmemesi için 
		[StringLength(100,MinimumLength =8)]
		public string Password { get; set; } = "";

		




	}
}

