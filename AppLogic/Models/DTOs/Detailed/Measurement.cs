﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Models.DTOs.Detailed
{
    /// <summary>
    /// Generic DTO for representing a measurement with a value and unit.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Measurement<T>
    {
        public T? Value { get; set; }
        public string? Unit { get; set; }

        
    }
}
