using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeAPI.Models;
using FridgeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingController : ControllerBase
    {
         private readonly ShoppingItemService _data;

        public ShoppingController(ShoppingItemService dataFromService)
        {
            _data = dataFromService;
        }

        //Add a shopping item to the database
        [HttpPost("AddShoppingItems")]
        public bool AddShoppingItem(ShoppingItemModel newShoppingItem)
        {
            return _data.AddShoppingItem(newShoppingItem);
        }

        //Get All Shopping Items
        [HttpGet("GetShoppingItems")]
        public IEnumerable<ShoppingItemModel> GetAllShoppingItems()
        {
            return _data.GetAllShoppingItems();
        }

        //Get shopping items by category
        [HttpGet("GetItemsByCategory/{Category}")]
        public IEnumerable<ShoppingItemModel> GetItemsByCategory(string Category)
        {
            return _data.GetItemsByCategory(Category);
        }

        //Update shopping item
        [HttpPost("UpdateShoppingItem")]
        public bool UpdateShoppingItems(ShoppingItemModel ItemUpdate)
        {
            return _data.UpdateShoppingItems(ItemUpdate);
        }

        //Delete shopping item
        [HttpPost("DeleteShoppingItem/{ShoppingItemToDelete}")]
        public bool DeleteShoppingItem(ShoppingItemModel ItemDelete)
        {
            return _data.DeleteShoppingItem(ItemDelete);
        }

        //Delete All shopping items
        [HttpPost("DeleteAllShoppingItems")]
        public bool DeleteAllShoppingItems()
        {
            return _data.DeleteAllShoppingItems();
        }
    }
}