using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManager.Models
{
    public class ItemCart
    {
        public Product shop_pro { get; set; }
        public int shop_quan { get; set; }
    }
    public class CartCK
    {
        List<ItemCart> items = new List<ItemCart>();
        public IEnumerable<ItemCart> Items { get { return items; } }
        public void Add(Product product, int quan = 1)
        {
            var item = items.FirstOrDefault(s => s.shop_pro.id_product == product.id_product);
            if (item == null)
            {
                items.Add(new ItemCart
                {
                    shop_pro = product,
                    shop_quan = quan
                });
            }
            else
            {
                item.shop_quan += quan;
            }
        }

        public void Update_quantity(int id, int quan)
        {
            var item = items.Find(s => s.shop_pro.id_product == id);
            if (item != null)
            {
                item.shop_quan = quan;
            }
        }
        public double Total_bill()
        {
            var total = items.Sum(s=>s.shop_pro.price*s.shop_quan);
            return total;
        }
        public void Remove(int id)
        {
            items.RemoveAll(s => s.shop_pro.id_product==id);    
        }

        //Tổng số lượng trong giỏ hàng
        public int Total_Quantity_in_CartCK()
        {
            return items.Sum(s => s.shop_quan);
        }
        public void ClearCartCK()
        {
            items.Clear();
        }
    }
}