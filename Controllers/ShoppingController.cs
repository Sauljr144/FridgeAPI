using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using FridgeAPI.Models;
using FridgeAPI.Services;
using FridgeAPI.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingController : ControllerBase
    {
        //  private readonly ShoppingItemService _data;

        // public ShoppingController(ShoppingItemService dataFromService)
        // {
        //     _data = dataFromService;
        // }

        // //Add a shopping item to the database
        // [HttpPost("AddShoppingItems")]
        // public bool AddShoppingItem(ShoppingItemModel newShoppingItem)
        // {
        //     return _data.AddShoppingItem(newShoppingItem);
        // }

        // //Get All Shopping Items
        // [HttpGet("GetShoppingItems")]
        // public IEnumerable<ShoppingItemModel> GetAllShoppingItems()
        // {
        //     return _data.GetAllShoppingItems();
        // }

        // //Get shopping items by category
        // [HttpGet("GetItemsByCategory/{Category}")]
        // public IEnumerable<ShoppingItemModel> GetItemsByCategory(string Category)
        // {
        //     return _data.GetItemsByCategory(Category);
        // }

        // //Update shopping item
        // [HttpPost("UpdateShoppingItem")]
        // public bool UpdateShoppingItems(ShoppingItemModel ItemUpdate)
        // {
        //     return _data.UpdateShoppingItems(ItemUpdate);
        // }

        // //Delete shopping item
        // [HttpPost("DeleteShoppingItem/{ShoppingItemToDelete}")]
        // public bool DeleteShoppingItem(ShoppingItemModel ItemDelete)
        // {
        //     return _data.DeleteShoppingItem(ItemDelete);
        // }

        // //Delete All shopping items
        // [HttpPost("DeleteAllShoppingItems")]
        // public bool DeleteAllShoppingItems()
        // {
        //     return _data.DeleteAllShoppingItems();
        // }

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



     
    }
}