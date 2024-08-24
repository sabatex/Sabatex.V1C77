// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models
{
    public class PeriodicValue<T>
    {
        public DateTime Date { get; set; }
        public T Value { get; set; }
    }
}
