﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class BrandModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string BrandLogo { get; set; } = null!;
        public int quantity { get; set; }
        public bool IsAvailable { get; set; }
        public object FunctionContent { get; set; } // Object type to hold any UI element
    }

    public class InsertBrandModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string BrandLogo { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }

    public class UpdateBrandModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string BrandLogo { get; set; } = null!;
    }
}
