using WebGoatCore.Models;
using WebGoatCore.Models.OrderDetailDomainPrimitives;
using WebGoatCore.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using NLog;

namespace WebGoatCore.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private readonly ProductRepository _productRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CartController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private Cart GetCart()
        {
            HttpContext.Session.TryGet<Cart>("Cart", out var cart);
            if(cart == null)
            {
                cart = new Cart();
            }
            return cart;
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }

        // [HttpPost("{productId}")]
        // public IActionResult AddOrder(int productId, [FromForm]short quantity)
        // {
        //     logger.Debug($"1quantity: {quantity}");
        //     if(quantity <= 0)
        //     {
        //         return RedirectToAction("Details", "Product", new { productId = productId, quantity = quantity });
        //     }

        //     var product = _productRepository.GetProductById(productId);
            
        //     var cart = GetCart();
        //     if(!cart.OrderDetails.ContainsKey(productId))
        //     {
        //         logger.Debug($"2quantity: {quantity}");
        //         var orderDetail = new OrderDetail()
        //         {
        //             ProductId = productId, 
        //             UnitPrice = product.UnitPrice, 
        //             Quantity = new Quantity(quantity),//, product.UnitsInStock), 
        //             Discount = 0.0F, 
        //             Product = product
        //         };
        //         cart.OrderDetails.Add(orderDetail.ProductId, orderDetail);
        //     }
        //     else
        //     {
        //         var originalOrder = cart.OrderDetails[productId];
        //         originalOrder.Quantity.AddAdditionalQuantity(quantity);
        //     }

        //     HttpContext.Session.Set("Cart", cart);

        //     return RedirectToAction("Index");
        // }
        [HttpPost("{productId}")]
        public IActionResult AddOrder(int productId, [FromForm] short quantity)
        {
            if (!ModelState.IsValid)
            {
                // Return an error message if the model is invalid
                return View("Error");
            }

            var product = _productRepository.GetProductById(productId);

            var cart = GetCart();
            if (!cart.OrderDetails.ContainsKey(productId))
            {
                var orderDetail = new OrderDetail()
                {
                    ProductId = productId,
                    UnitPrice = product.UnitPrice,
                    Quantity = new Quantity(quantity),
                    Discount = 0.0F,
                    Product = product
                };
                cart.OrderDetails.Add(orderDetail.ProductId, orderDetail);
            }
            else
            {
                var originalOrder = cart.OrderDetails[productId];
                originalOrder.Quantity.AddAdditionalQuantity(quantity);
            }

            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index");
        }


        [HttpGet("{productId}")]
        public IActionResult RemoveOrder(int productId)
        {
            try
            {
                var cart = GetCart();
                if (!cart.OrderDetails.ContainsKey(productId))
                {
                    return View("RemoveOrderError", string.Format("Product {0} was not found in your cart.", productId));
                }

                cart.OrderDetails.Remove(productId);
                HttpContext.Session.Set("Cart", cart);

                Response.Redirect("~/ViewCart.aspx");
            }
            catch (Exception ex)
            {
                return View("RemoveOrderError", string.Format("An error has occurred: {0}", ex.Message));
            }

            return RedirectToAction("Index");
        }
    }
}