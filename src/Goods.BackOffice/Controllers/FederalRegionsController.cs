using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers;

public class FederalRegionsController(IFederalRegionsService federalRegionsService) : BaseController
{
	[HttpGet("/federal-regions")]
    public IActionResult Index() => ReactApp();

	[HttpPost("federal-regions/save")]
	public Task<Result> SaveProducts([FromBody] FederalRegionsBlank blank)
	{
		return federalRegionsService.SaveFederalRegion(blank);
	}

	[HttpGet("federal-regions/get-page")]
	public Task<Page<FederalRegions>> GetProductsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return federalRegionsService.GetFederalRegions(page, count);
	}

	[HttpGet("federal-regions/get-by-id")]
	public Task<FederalRegions> GetProduct([FromQuery] Guid id)
	{
		return federalRegionsService.GetFederalRegion(id);
	}

	[HttpPost("federal-regions/remove")]
	public Task<Result> RemoveProduct([FromQuery] Guid id)
	{
		return federalRegionsService.RemoveFederalRegion(id);
	}
}
