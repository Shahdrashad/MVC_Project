﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demo.DAl.Models
{
	public class ApplicationUser:IdentityUser
	{
		public string FirstName { get; set; }
		
		public string LastName { get; set; }

		public bool IsAgree { get; set; }
	}
}