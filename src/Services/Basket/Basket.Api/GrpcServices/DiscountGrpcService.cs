using Discount.Grpc.Protos;
using Grpc.Core;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceBase _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceBase discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };

            return await _discountProtoService.GetDiscount(discountRequest,default(ServerCallContext));
        }
    }
}