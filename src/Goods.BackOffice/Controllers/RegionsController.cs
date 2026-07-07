using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers;

public class RegionsController(IRegionsService regionsService) : BaseController
{
    [HttpGet("/regions")]
    public IActionResult Index() => ReactApp();

    [HttpPost("regions/save")]
	public Task<Result> SaveProducts([FromBody] RegionsBlank blank)
	{
		return regionsService.SaveRegion(blank);
	}

	[HttpGet("regions/get-page")]
	public Task<Page<Regions>> GetProductsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return regionsService.GetRegions(page, count);
	}

	[HttpGet("regions/get-by-id")]
	public Task<Regions> GetProduct([FromQuery] Guid id)
	{
		return regionsService.GetRegion(id);
	}

	[HttpPost("regions/remove")]
	public Task<Result> RemoveProduct([FromQuery] Guid id)
	{
		return regionsService.RemoveRegion(id);
	}
}
