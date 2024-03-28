using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JetFlix_API.Models
{
	public class Category
	{
		
		public short Id { get; set; }

		[Column(TypeName="nvarchar(50)")]
		[StringLength(50,MinimumLength =2)]
		public string Name { get; set; } = "";

	}
}

