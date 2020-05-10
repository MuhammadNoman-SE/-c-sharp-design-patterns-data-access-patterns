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

        public virtual string Name { get; set; }
        public virtual string ShippingAddress { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }

        //public  Lazy<byte[]> ProfilePictureValuHolder { get; set; }
        //public byte[] ProfilePicture {
        //    get {
        //        return ProfilePictureValuHolder.Value;
        //        }
        //}

        public virtual byte[] ProfilePicture { get; set; }
    public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
