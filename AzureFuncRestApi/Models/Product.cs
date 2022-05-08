using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFuncRestApi.Models
{
    public class Product : BaseModel
    {
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public int Price { get; set; }
    }
}
