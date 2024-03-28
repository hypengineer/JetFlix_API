using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetFlix_API.Models
{
	public class Person
	{
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = "";
    }
}

