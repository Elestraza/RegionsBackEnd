using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers;

public class SettlementsController(ISettlementsService settlementsService) : BaseController
{
    [HttpGet("/settlements")]
    public IActionResult Settlements() => ReactApp();

    [HttpPost("settlements/save")]
	public Task<Result> SaveSettlements([FromBody] SettlementsBlank blank)
	{
		return settlementsService.SaveSettlement(blank);
	}

	[HttpGet("settlements/get-page")]
	public Task<Page<Settlements>> GetSettlementPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return settlementsService.GetSettlements(page, count);
	}

    [HttpGet("settlements/get-by-id")]
	public Task<Settlements> GetSettlement([FromQuery] Guid id)
	{
		return settlementsService.GetSettlement(id);
	}

    [HttpGet("settlements/settlement-history-value/get-by-id")]
    public Task<String> GetSettlementHistoryValue([FromQuery] Guid id)
    {
        return settlementsService.GetSettlementHistoryValue(id);
    }

    [HttpPost("settlements/remove")]
	public Task<Result> RemoveSettlement([FromQuery] Guid id)
	{
		return settlementsService.RemoveSettlement(id);
	}
}
