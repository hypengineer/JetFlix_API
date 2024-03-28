using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetFlix_API.Models
{
	public class MediaCategory
	{
		public int MediaId { get; set; }
		[ForeignKey("MediaId")]
		public Media? Media  { get; set; }


		public short CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public Category? Category { get; set; }
	}
}

