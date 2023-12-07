using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeAPI.Models;
using FridgeAPI.Services.Context;

namespace FridgeAPI.Services
{
    public class ShoppingItemService
    {
        private readonly DataContext _context;

        public ShoppingItemService(DataContext context)
        {
            _context = context;
        }

        //Add a shopping item to the database
        public bool AddShoppingItem(ShoppingItemModel newShoppingItem)
        {
            bool result = false;
            _context.Add(newShoppingItem);

            result = _context.SaveChanges() != 0;
            return result;
        }

        //Get All Shopping Items
        public IEnumerable<ShoppingItemModel> GetAllShoppingItems()
        {
            return _context.ShoppingItemInfo;
        }

        //Get Shopping items by category
        public IEnumerable<ShoppingItemModel> GetItemsByCategory(string Category)
        {
            return _context.ShoppingItemInfo.Where(item => item.Category == Category);
        }

        //Update Shopping item
        public bool UpdateShoppingItems(ShoppingItemModel ItemUpdate)
        {
            _context.Update<ShoppingItemModel>(ItemUpdate);
            return _context.SaveChanges() != 0;
        }

        //Delete Shopping item
        public bool DeleteShoppingItem(ShoppingItemModel ItemDelete)
        {
            _context.Update<ShoppingItemModel>(ItemDelete);
            return _context.SaveChanges() != 0;
        }

        //Delete All Shopping items
        public bool DeleteAllShoppingItems()
        {
            _context.RemoveRange(_context.ShoppingItemInfo);
            return _context.SaveChanges() != 0;
        }

    }
}