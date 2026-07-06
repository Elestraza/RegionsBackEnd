using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Products;

public class SettlementsController(ISettlementsService settlementsService) : BaseController
{
	[HttpPost("settlements/save")]
	public Task<Result> SaveProducts([FromBody] SettlementsBlank blank)
	{
		return settlementsService.SaveSettlement(blank);
	}

	[HttpGet("settlements/get-page")]
	public Task<Page<Settlements>> GetProductsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return settlementsService.GetSettlements(page, count);
	}

	[HttpGet("settlements/get-by-id")]
	public Task<Settlements> GetProduct([FromQuery] Guid id)
	{
		return settlementsService.GetSettlement(id);
	}

	[HttpGet("settlements/remove")]
	public Task<Result> RemoveProduct([FromQuery] Guid id)
	{
		return settlementsService.RemoveSettlement(id);
	}
}
