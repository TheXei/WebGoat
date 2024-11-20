using System;
using System.ComponentModel.DataAnnotations;
using WebGoat.NET.Utils;
using WebGoatCore.Models.OrderDetailDomainPrimitives;
using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }

        private short quantity;

        [JsonConverter(typeof(QuantityConverter))]
        public Quantity Quantity
        {
            get
            {
                // Ensure the Product is loaded before accessing UnitsInStock
                //if (Product == null)
                //    return new Quantity(quantity);//, 10);

                //return new Quantity(quantity);//, Product.UnitsInStock);
                return new(quantity);
            }
            set
            {
                quantity = value.Value; // Store only the value for EF
            }
        }
        public float Discount { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        // private OrderDetail(int productId, double unitPrice, short quantity, float discount)
        // {
        //     ProductId = productId;
        //     UnitPrice = unitPrice;
        //     Quantity = new Quantity(quantity, Product.UnitsInStock);
        //     Discount = discount;
        // }
        // public OrderDetail(int productId, double unitPrice, short quantity, float discount,Product product) : this(productId, unitPrice, quantity, discount)
        // {
        //     Product = product;
        // }

        public decimal DecimalUnitPrice => Convert.ToDecimal(this.UnitPrice);
        public decimal ExtendedPrice => DecimalUnitPrice * Convert.ToDecimal(1 - Discount) * quantity;
    }
}
