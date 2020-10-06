using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApi.Models
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Not Empty")]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Company { get; set; }
    }
}
