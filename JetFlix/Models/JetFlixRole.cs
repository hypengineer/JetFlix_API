using System;
using Microsoft.AspNetCore.Identity;

namespace JetFlix_API.Models
{
	public class JetFlixRole:IdentityRole<long>
	{
		public JetFlixRole(): base()
		{

		}

        public JetFlixRole(string roleName) : base(roleName)
        {

        }

    }
}

