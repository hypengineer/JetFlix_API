﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetFlix_API.Models
{
	public class MediaDirector
	{
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }


        public int DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public Director? Director { get; set; }
    }
}

