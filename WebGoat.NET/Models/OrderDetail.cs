using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        //public short Quantity { get; set; }
        private short _quantity = 1;

        public short Quantity
        {
            get { return _quantity; }
            set
            {
                try
                {
                    if (value <= 0)
                        throw new ArgumentOutOfRangeException(nameof(Quantity));
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
                finally
                {
                     _quantity = value;
                }

            }
        }

        public float Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public decimal DecimalUnitPrice => Convert.ToDecimal(this.UnitPrice);
        public decimal ExtendedPrice => DecimalUnitPrice * Convert.ToDecimal(1 - Discount) * Quantity;
    }
}
