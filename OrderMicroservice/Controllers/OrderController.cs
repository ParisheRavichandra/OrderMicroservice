using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderManagement.API.DTOs;
using OrderManagement.Domain.Aggregates.OrderAggregate;
using OrderManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        private readonly IRepository<Order> OrderRepository;
        public OrderController(IRepository<Order> OrderRepository)
        {
            this.OrderRepository = OrderRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<OrderDTO>))]
        public IActionResult GetOrderItems()
        {
            var Orders = OrderRepository.Get();
            var dtos = from order in Orders
                       select new OrderDTO
                       {
                           Id = order.Id,
                           Order_Id = order.Order_Id,
                           User_Id = order.User_Id,
                           Order_Date = order.Order_Date,
                           Bill_Amount = order.Bill_Amount,
                           Book_Name = order.Book_Name,
                           Book_Type = order.Book_Type,
                           Quantity = order.Quantity,
                           
                           
                       };
            return Ok(dtos);
        }

        [HttpGet]
        [Route("/Order_Item")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Order_ItemDTO))]
        public async Task<IActionResult> UpdateOrderAsync()
        {
            //To generate orderid
            Guid orderid = Guid.NewGuid();
            string order_id = orderid.ToString();
            Debug.WriteLine(order_id);

            //To get values from Cart Table
            var response = await client.GetAsync("http://localhost:40825/api/Cart");
            var responseBody = await response.Content.ReadAsStringAsync();
            var order_item = JsonConvert.DeserializeObject<List<Order_Item>>(responseBody);

            foreach (var item in order_item)
            {
                Order_Item order_Item = new Order_Item
                {
                    Order_Id = order_id,
                    Book_Name = item.Book_Name,
                    Book_Price = item.Book_Price,
                    Book_Type = item.Book_Type,
                    Quantity = item.Quantity

                };
                OrderRepository.Add(order_Item);

            }
            Debug.WriteLine("Cart Table Values:" + responseBody.ToString());
            await OrderRepository.SaveAsync();

            //Save values from Order_Item to Order Table
            Debug.WriteLine("Start to update");



            foreach (var data in order_item)
            {

                Order orders = new Order
                {
                    Order_Id = order_id,
                    Order_Date = DateTime.Now,
                    User_Id = 971324,
                    Bill_Amount = data.Book_Price * data.Quantity,
                    Book_Name = data.Book_Name,
                    Quantity = data.Quantity,
                    Book_Type = data.Book_Type,
                    

                };
                OrderRepository.Add(orders);

            }
            Debug.WriteLine("OrderItem Table Values:" + responseBody.ToString());

            await OrderRepository.SaveAsync();

            Debug.WriteLine("Order updated");
            return Ok("Order Placed Successfully");

        }
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(OrderDTO))]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetFoodItemsById(int id)
        {
            var order_item = OrderRepository.GetById(id);
            if (order_item == null)
                return NotFound();
            return Ok(order_item);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = (typeof(OrderDTO)))]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderDTO dto)
        {
            var order_status = OrderRepository.GetById(id);
            if (order_status == null)
                return NotFound();

            order_status.Order_Id = dto.Order_Id;
            order_status.Order_Date = dto.Order_Date;
            order_status.User_Id = dto.User_Id;
            order_status.Bill_Amount = dto.Bill_Amount;
            
            order_status.Book_Name = dto.Book_Name;
            order_status.Quantity = dto.Quantity;
          
            order_status.Book_Type = dto.Book_Type;
         
            OrderRepository.Update(order_status);
            await OrderRepository.SaveAsync();
            return Ok(order_status);
        }
    }
}