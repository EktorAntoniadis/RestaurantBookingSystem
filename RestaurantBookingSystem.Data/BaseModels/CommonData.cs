﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.BaseModels
{
    public class CommonData
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
