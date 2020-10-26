using Grpc.Core;
using Microsoft.Extensions.Logging;
using ShoppingCartGrpc.Data;
using ShoppingCartGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartGrpc.Services
{
    public class ShoppingCartService : ShoppingCartProtoService.ShoppingCartProtoServiceBase
    {
        private readonly ShoppingCartContext _shoppingCartDbContext;        
        private readonly ILogger<ShoppingCartService> _logger;

        public ShoppingCartService(ShoppingCartContext shoppingCartDbContext, ILogger<ShoppingCartService> logger)
        {
            _shoppingCartDbContext = shoppingCartDbContext ?? throw new ArgumentNullException(nameof(shoppingCartDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override Task<ShoppingCartModel> GetShoppingCart(GetShoppingCartRequest request, ServerCallContext context)
        {
            return base.GetShoppingCart(request, context);
        }

        public override Task<AddShoppingCartResponse> AddShoppingCart(IAsyncStreamReader<ShoppingCartModel> requestStream, ServerCallContext context)
        {
            return base.AddShoppingCart(requestStream, context);
        }

        public override Task<RemoveShoppingCartResponse> RemoveShoppingCart(RemoveShoppingCartRequest request, ServerCallContext context)
        {
            return base.RemoveShoppingCart(request, context);
        }
    }
}
