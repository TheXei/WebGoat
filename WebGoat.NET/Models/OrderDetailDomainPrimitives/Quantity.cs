using System;

namespace WebGoatCore.Models.OrderDetailDomainPrimitives
{
    public class Quantity
    {
        public short Value {get; private set;}
        private const short minimumQuantity = 1;
        // private short maximumQuantity;
    
        public Quantity(short quantity)//, short quantityInStock)
        {
            //maximumQuantity = quantityInStock;
            ValidateQuantity(quantity);
            Value = quantity;
        }
        private void ValidateQuantity(short quantity)
        {
            if (IsLessThanMinimum(quantity))
            {
                throw new ArgumentOutOfRangeException($"Quantity cannot be less than {minimumQuantity}, quantity is {quantity}");
            }

            // if (IsLargerThanMaximum(quantity))
            // {
            //     throw new ArgumentOutOfRangeException($"Quantity cannot be larger than {maximumQuantity}");
            // }
        }
        private bool IsLessThanMinimum(short quantity) { return quantity < minimumQuantity; }
        // private bool IsLargerThanMaximum(short quantity) { return quantity > maximumQuantity; }

        public void AddAdditionalQuantity(short quantityToAdd){
            short updatedQuantity= (short)(Value + quantityToAdd);
            ValidateQuantity(updatedQuantity);
            Value = updatedQuantity;
        }

        public override string ToString() => Value.ToString();
    }

}