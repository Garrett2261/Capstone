﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class EmailInformation
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ToAddress { get; set; }
    }
}