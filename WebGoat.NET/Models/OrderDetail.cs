using System;
using System.ComponentModel.DataAnnotations;
using WebGoatCore.Models.OrderDetailDomainPrimitives;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        private short _quantityValue;
        public Quantity Quantity
        {
            get
            {
                // Ensure the Product is loaded before accessing UnitsInStock
                if (Product == null)
                    return new Quantity(_quantityValue);//, 10);

                return new Quantity(_quantityValue);//, Product.UnitsInStock);
            }
            set
            {
                _quantityValue = value.Value; // Store only the value for EF
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
        public decimal ExtendedPrice => DecimalUnitPrice * Convert.ToDecimal(1 - Discount) * _quantityValue;
    }
}
