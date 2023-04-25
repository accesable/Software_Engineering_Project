using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilePhoneDistributor_Web.Models
{
    public class Cart
    {
        private List<OrderDetail> items = new List<OrderDetail>();

        public void AddItem(OrderDetail item)
        {
            var existingItem = items.FirstOrDefault(i => i.PhoneModelId == item.PhoneModelId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                items.Add(item);
            }
        }

        public void RemoveItem(string id)
        {
            var itemToRemove = items.FirstOrDefault(i => i.PhoneModelId == id);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        public List<OrderDetail> GetItems()
        {
            return items;
        }
    }
}