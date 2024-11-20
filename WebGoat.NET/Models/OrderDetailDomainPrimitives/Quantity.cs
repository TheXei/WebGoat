using System;

namespace WebGoatCore.Models.OrderDetailDomainPrimitives
{
    public class Quantity
    {
        private short _quantity;
        private const short minimumQuantity = 1;
        private short maximumQuantity;
    
        public Quantity(short quantity, short quantityInStock)
        {
            maximumQuantity = quantityInStock;
            ValidateQuantity(quantity);
            _quantity = quantity;
        }
        private void ValidateQuantity(short quantity)
        {
            if (IsLessThanMinimum(quantity))
            {
                throw new ArgumentOutOfRangeException($"Quantity cannot be less than {minimumQuantity}");
            }

            if (IsLargerThanMaximum(quantity))
            {
                throw new ArgumentOutOfRangeException($"Quantity cannot be larger than {maximumQuantity}");
            }
        }
        private bool IsLessThanMinimum(short quantity) { return quantity < minimumQuantity; }
        private bool IsLargerThanMaximum(short quantity) { return quantity > maximumQuantity; }

        public short GetValue() => _quantity;

        public void AddAdditionalQuantity(short quantityToAdd){
            short updatedQuantity= (short)(_quantity + quantityToAdd);
            ValidateQuantity(updatedQuantity);
            _quantity = updatedQuantity;

        }
    }

}