using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetFlix_API.Models
{
	public class UserWatched
	{
		public long UserId { get; set; }
		[ForeignKey("UserId")]
		public JetFlixUser? JetFlixUser { get; set; }


		public long EpisodeId { get; set; }
		[ForeignKey("EpisodeId")]
		public Episode? Episode { get; set; }
	}
}

