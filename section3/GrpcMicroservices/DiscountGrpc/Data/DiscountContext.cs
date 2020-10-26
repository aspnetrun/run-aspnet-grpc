using DiscountGrpc.Models;
using System.Collections.Generic;

namespace DiscountGrpc.Data
{
    public static class DiscountContext
    {
        public static readonly List<Discount> Discounts = new List<Discount>
        {
            new Discount{DiscountId = 1, Code = "CODE_100", Amount = 100 },
            new Discount{DiscountId = 1, Code = "CODE_200", Amount = 200 },
            new Discount{DiscountId = 1, Code = "CODE_300", Amount = 300 }
        };
    }
}
