using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApi.Models
{
    public class CustomerInfo
    {
        public int CustID { get; set; }
        [Required(ErrorMessage = "Not Empty")]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
