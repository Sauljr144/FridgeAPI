using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeAPI.Models
{
    public class FridgeItemModel
    {
        public string? FridgeItemName { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string? ExpirationDate { get; set; }
        public string? Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}