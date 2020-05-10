using MyShop.Domain.Models;
using MyShop.Infrastructure.sevices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Lazy.Proxy
{
    public class CustomerProxy:Customer
    {
        public override byte[] Pp {
            get {
                if (null == base.Pp)
                    base.Pp = ProfilePictureService.GetFor(base.Name);
                return base.Pp;
            }
        }
    }
}
