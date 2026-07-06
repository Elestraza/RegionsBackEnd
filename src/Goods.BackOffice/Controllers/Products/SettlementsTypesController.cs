using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Products;

public class SettlementsTypesController(ISettlementsTypesService settlementsTypesService) : BaseController
{
	[HttpGet("/settlements-types")]
    public IActionResult Index() => ReactApp();

	[HttpPost("settlements-types/save")]
	public Task<Result> SaveProducts([FromBody] SettlementsTypesBlank blank)
	{
		return settlementsTypesService.SaveSettlementsType(blank);
	}

	[HttpGet("settlements-types/get-page")]
	public Task<Page<SettlementsTypes>> GetProductsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return settlementsTypesService.GetSettlementsTypes(page, count);
	}

	[HttpGet("settlements-types/get-by-id")]
	public Task<SettlementsTypes> GetProduct([FromQuery] Guid id)
	{
		return settlementsTypesService.GetSettlementsType(id);
	}

	[HttpGet("settlements-types/remove")]
	public Task<Result> RemoveProduct([FromQuery] Guid id)
	{
		return settlementsTypesService.RemoveSettlementsType(id);
	}
}
