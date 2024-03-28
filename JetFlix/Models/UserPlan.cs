using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetFlix_API.Models
{
	public class UserPlan
	{
		public long Id { get; set; }

		[Column(TypeName ="date")]
		public DateTime StartDate { get; set; }

		[Column(TypeName ="date")]
		public DateTime EndDate { get; set; }



		public long UserId { get; set; }
		[ForeignKey("UserId")]
		public JetFlixUser? JetFlixUser { get; set; }

		public short PlanId { get; set; }
		[ForeignKey("PlanId")]
		public Plan? Plan { get; set; }
	}
}

