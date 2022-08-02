using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs;
using OrderManagement.Domain.Aggregates.OrderAggregate;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IRepository<cart> CartRepository;
        public CartController(IRepository<cart> CartRepository)
        {
            this.CartRepository = CartRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CartDTO>))]
        public IActionResult GetCartItems()
        {
            var Cart = CartRepository.Get();
            var dtos = from cart in Cart
                       select new CartDTO
                       {
                           Id = cart.Id,
                           Book_Name = cart.Book_Name,
                           Book_Type = cart.Book_Type,
                           Book_Price = cart.Book_Price,
                           Quantity = cart.Quantity,


                       };
            return Ok(dtos);

        }
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CartDTO))]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetFoodItemsById(int id)
        {
            var food_item = CartRepository.GetById(id);
            if (food_item == null)
                return NotFound();
            return Ok(food_item);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]

        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = CartRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            CartRepository.Remove(item);
            await CartRepository.SaveAsync();
            return StatusCode(204);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = (typeof(CartDTO)))]
        public async Task<IActionResult> UpdateFoodItems(int id, [FromBody] CartDTO dto)
        {
            var food_item = CartRepository.GetById(id);
            if (food_item == null)
                return NotFound();


            food_item.Quantity = dto.Quantity;
            CartRepository.Update(food_item);
            await CartRepository.SaveAsync();
            return Ok(food_item);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateItems(CartDTO dto)
        {
            var cart_items = new cart(dto.Book_Name, dto.Book_Type, dto.Book_Price,dto.Quantity);
            CartRepository.Add(cart_items);
            await CartRepository.SaveAsync();
            return StatusCode(201);
        }

    }
}

