using System;
using System.Dynamic;

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

        //public Lazy<byte[]> ProilePictureHolder { get; set; }

        //public byte[] Pp  {
        //get{
        //        return ProilePictureHolder.Value;
        //    }
        //}
        public virtual byte[] Pp { get; set; }
        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
