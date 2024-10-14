using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Models
{
   public class Employee
    {
        public int Id { get; set; }

        [Required]
        
      
        public string Name { get; set; }

        [Range(22,45)]
        public int? Age { get; set; }
                             //  123   - street      -   city        -    country
        //[RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5-10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="Address must be like 123-street-city-country")]
        public string Address {  get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone {  get; set; }

       
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [DisplayName]
        public DateTime HireDate { get; set; }
        [DisplayName    ("Date Of Creation")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
       
        public Department? Departments { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }


    }
}
