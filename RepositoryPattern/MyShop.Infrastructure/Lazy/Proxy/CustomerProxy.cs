using MyShop.Domain.Models;
using MyShop.Infrastructure.sevices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Lazy.Proxy
{
    public class CustomerProxy:Customer
    {
        public override byte[] ProfilePicture
        {
            get {
                if (base.ProfilePicture == null) {
                    base.ProfilePicture = ProfilePictureService.GetFor(Name);
                }
                return base.ProfilePicture;
            }
        }
    }
}
