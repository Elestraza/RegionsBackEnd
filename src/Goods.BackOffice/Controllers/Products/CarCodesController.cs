using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Products;

public class CarCodesController(ICarCodesService carCodesService) : BaseController
{
	[HttpGet("/car-codes")]
    public IActionResult Index() => ReactApp();

	[HttpPost("car-codes/save")]
	public Task<Result> SaveProducts([FromBody] CarCodesBlank blank)
	{
		return carCodesService.SaveCarCode(blank);
	}

	[HttpGet("car-codes/get-page")]
	public Task<Page<CarCodes>> GetProductsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return carCodesService.GetCarCodes(page, count);
	}

	[HttpGet("car-codes/get-by-id")]
	public Task<CarCodes> GetProduct([FromQuery] Guid id)
	{
		return carCodesService.GetCarCode(id);
	}

	[HttpGet("car-codes/remove")]
	public Task<Result> RemoveProduct([FromQuery] Guid id)
	{
		return carCodesService.RemoveCarCode(id);
	}
}
