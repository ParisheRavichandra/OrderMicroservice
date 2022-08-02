using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.DTOs
{
    public class Order_ItemDTO
    {
        public long Id { get; set; }
        public string Order_Id { get; set; }
        [Required, StringLength(30)]
        public string Book_Name { get; set; }
        [Required, StringLength(30)]
        public string Book_Type { get; set; }
        [Required]
        public decimal Book_Price { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
