using DiscountGrpc.Models;
using System.Collections.Generic;

namespace DiscountGrpc.Data
{
    public static class DiscountContext
    {
        public static readonly List<Discount> Discounts = new List<Discount>
        {
            new Discount{DiscountId = 1, Code = "CODE_X", Amount = 10 },
            new Discount{DiscountId = 1, Code = "CODE_Y", Amount = 20 },
            new Discount{DiscountId = 1, Code = "CODE_Z", Amount = 30 }
        };
    }
}
