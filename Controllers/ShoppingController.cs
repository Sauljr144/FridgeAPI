using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using FridgeAPI.Models;
using FridgeAPI.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingController : ControllerBase
    {

        private readonly DataContext _context;

        public ShoppingController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ShoppingItemModel>> GetShoppingItems()
        {
            var shoppingItems = await _context.ShoppingItemInfo.AsNoTracking().ToListAsync();
            return shoppingItems;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShoppingItemModel shoppingItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAsync(shoppingItem);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditShoppingItem(int id, ShoppingItemModel shoppingItem)
        {
            var itemStored = await _context.ShoppingItemInfo.FindAsync(id);
            if(itemStored == null)
            {
                return BadRequest();
            }

            itemStored.ShoppingItemName = shoppingItem.ShoppingItemName;
            itemStored.Quantity = shoppingItem.Quantity;
            itemStored.ExpirationDate = shoppingItem.ExpirationDate;
            itemStored.Category = shoppingItem.Category;
            itemStored.IsDeleted = shoppingItem.IsDeleted;

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            var itemStored = await _context.ShoppingItemInfo.FindAsync(id);
            if(itemStored == null)
            {
                return BadRequest();
            }

            _context.Remove(itemStored);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllShoppingItems()
        {
            var itemsStored = await _context.ShoppingItemInfo.ToListAsync();
            if(itemsStored == null)
            {
                return BadRequest();
            }

            _context.RemoveRange(itemsStored);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
     
    }
}