﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Dto
{
    public class PromosDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsEnabled { get; set; }
    }
}
