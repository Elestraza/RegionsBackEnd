using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers;

public class FederalRegionsController(IFederalRegionsService federalRegionsService) : BaseController
{
    [HttpGet("/federal-regions")]
    public IActionResult FederalRegions() => ReactApp();

    [HttpPost("federal-regions/save")]
	public Task<Result> SaveSettlements([FromBody] FederalRegionsBlank blank)
	{
		return federalRegionsService.SaveFederalRegion(blank);
	}

	[HttpGet("federal-regions/get-page")]
	public Task<Page<FederalRegions>> GetSettlementPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return federalRegionsService.GetFederalRegions(page, count);
	}

    [HttpGet("federal-regions/get-by-id")]
	public Task<FederalRegions> GetProduct([FromQuery] Guid id)
	{
		return federalRegionsService.GetFederalRegion(id);
	}

	[HttpPost("federal-regions/remove")]
	public Task<Result> RemoveSettlement([FromQuery] Guid id)
	{
		return federalRegionsService.RemoveFederalRegion(id);
	}
}
