using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string MailAddress { get; set; }
        public string MobileAddress { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public DateTime ModifiedTime { get; set; }

        public string LastName { get; set; }
    }
}
