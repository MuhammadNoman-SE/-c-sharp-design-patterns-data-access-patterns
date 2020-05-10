using MyShop.Domain.Lazy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MyShop.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string Name { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public  Lazy<byte[]> ProfilePictureValuHolder { get; set; }
        public byte[] ProfilePicture {
            get {
                return ProfilePictureValuHolder.Value;
                }
        }

        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
