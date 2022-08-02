using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.API.DTOs
{
    public class CartDTO
    {
        public long Id { get; set; }
        [Required, StringLength(30)]
        public  string Book_Name { get;  set; }
        [Required, StringLength(30)]
        public  string Book_Type { get;  set; }
        [Required]
        public  decimal Book_Price { get;  set; }
        [Required]
        public int Quantity { get; set; }
       

      
    }
}