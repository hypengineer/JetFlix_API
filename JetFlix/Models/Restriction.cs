using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetFlix_API.Models
{
	public class Restriction
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
		public byte Id { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = "";
    }
}

