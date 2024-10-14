using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Models
{
	public class Department
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Name is Required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "code is Required")]
		public string Code { get; set; }
		public DateTime DateOfCreation { get; set; }

		public ICollection<Employee> Employees { get; set; }=new List<Employee>();

	}
}
