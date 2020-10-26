using DiscountGrpc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountGrpc.Data
{
    public class DiscountContext
    {
        public DiscountContext()
        {

        }

        public List<Discount> Discount { get; set; }
    }
}
