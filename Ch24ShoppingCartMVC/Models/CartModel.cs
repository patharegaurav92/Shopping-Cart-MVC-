﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ch24ShoppingCartMVC.Models;
using System.Diagnostics;

namespace Ch24ShoppingCartMVC.Models{
    public class CartModel
    {
        //Data Access methods 
        private List<ProductViewModel> GetCartFromDataStore()
        {
            List<ProductViewModel> cart;
            object objCart = HttpContext.Current.Session["cart"];
            //Convert objCart to List<ProductViewModel>
            cart = (List<ProductViewModel>)objCart;
            if (cart == null)
            {   //Create the object cart
                cart = new List<ProductViewModel>();
                //Assign cart to the Session object cart
                HttpContext.Current.Session["cart"] = cart;
            }
            return cart;
        }
        private ProductViewModel GetSelectedProduct(string id)
        {   //Create an OrderModel object called order
            OrderModel order = new OrderModel();
            //Call the method GetSelectedProduct of the class OrderModel. Return the object returned by the call.
            return order.GetSelectedProduct(id);
        }
        public CartViewModel GetCart(string id = "")
        {
            CartViewModel model = new CartViewModel();
            //Call the method GetCartFromDataStore
           List<ProductViewModel> mod =  GetCartFromDataStore();
            model.Cart = mod;
            if (!string.IsNullOrEmpty(id))
                //Called the method GetSelectedProduct with parameter id and assign the return object to the AddedProduct
                model.AddedProduct = GetSelectedProduct(id);
            return model;
        }
        public void DeleteCart()
        {
            HttpContext.Current.Session["cart"] = null;
        }
        private void AddItemToDataStore(CartViewModel model)
        {   //Add the AddedProduct to the cart
            //List<ProductViewModel> cart = GetCartFromDataStore();
            //cart.Add(model.AddedProduct);


            List<ProductViewModel> cart =(List<ProductViewModel>)HttpContext.Current.Session["cart"];
            cart.Add(model.AddedProduct);
            HttpContext.Current.Session["cart"] = cart;
        }
        public void AddToCart(CartViewModel model)
        {
            if (model.AddedProduct.ProductID != null)
            {
                //Get the product id of the added product
                String id = model.AddedProduct.ProductID;
                Debug.WriteLine("The Output is");
                //Find the product in the car that matches the id using lambda expression.
                var inCart = model.Cart.Where(a => a.ProductID==id).SingleOrDefault();
                if (inCart == null)
                    //Call the method AddItemToDataStore
                    AddItemToDataStore(model);
                else
                {
                    //Increase the Quantity by the quantity of the added product
                    inCart.Quantity += model.AddedProduct.Quantity;
                    

                }
            }
        }
                
       
    }    
}