using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.CoreBusiness.Models
{
    public class OrderLineItem
    {
        public int? OrderLineItemId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? OrderId { get; set; }

        private double _price;
        private int _quantity;
        private double _totalPrice;

        public double Price
        {
            get { return _price; }
            set {
                _price = value;
                UpdateTotalPrice();
            }
        }


        public int Quantity
        {
            get { return _quantity; }
            set {
                _quantity = value;
                UpdateTotalPrice();
            }
        }


        public double TotalPrice
        {
            get { return _totalPrice; }
            private set { _totalPrice = value; }
        }

        private void UpdateTotalPrice()
        {
            _totalPrice = _price * _quantity;
        }

    }
}
