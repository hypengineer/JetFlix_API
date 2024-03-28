using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetFlix_API.Models
{
	public class UserFavorite // Favorite classı oluşturmadık favoriler için User ile mediaları bağlamak yeterli olur
	{
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public JetFlixUser? JetFlixUser { get; set; }


        public int MediId { get; set; }
        [ForeignKey("MediId")]
        public Media? Media { get; set; }
    }
}

