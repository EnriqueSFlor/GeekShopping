using GeekShopping.CouponAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {

        private ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet("couponCode")]
        [Authorize]
        public async Task<IActionResult> FindById(string couponCode)
        {
            var coupon = await _repository.GetCouponByCouponCode(couponCode);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }


    }
}