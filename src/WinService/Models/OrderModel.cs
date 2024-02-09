﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinService.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Contractor { get; set; } = string.Empty;
    }
}
