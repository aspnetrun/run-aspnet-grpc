using DiscountGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartGrpc.Services
{
    public class DiscountService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<DiscountModel> GetDiscount(string discountCode)
        {
            var discountRequest = new GetDiscountRequest { DiscountCode = discountCode };

            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
