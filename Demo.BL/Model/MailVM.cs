﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Model
{
    public class MailVM
    {
        [Required(ErrorMessage ="Title Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Message Required")]
        [EmailAddress]
        public string To { get; set; }
    }
}
