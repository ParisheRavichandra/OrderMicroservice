using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.API.DTOs
{
    public class OrderDTO
    {


        public long Id { get; set; }
        public string Order_Id { get; set; }
        public int User_Id { get; set; }

        [Required]
        public DateTime Order_Date { get; set; }

        [Required]
        public decimal Bill_Amount { get; set; }

        [Required, StringLength(30)]
        
        public string Book_Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required, StringLength(30)]
        
        public string Book_Type { get; set; }

        
    }
}