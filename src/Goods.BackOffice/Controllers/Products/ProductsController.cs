using Goods.BackOffice.Controllers.Infrastructure;
using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goods.BackOffice.Controllers.Products;

public class ProductsController(ISettlementsService productsService) : BaseController
{
	[HttpGet("/products")]
    public IActionResult Index() => ReactApp();

	[HttpPost("products/save")]
	public Task<Result> SaveProducts([FromBody] SettlementsBlank productBlank)
	{
		return productsService.SaveSettlement(productBlank);
	}

	[HttpGet("products/get-page")]
	public Task<Page<Settlements>> GetProductsPage([FromQuery] Int32 page, [FromQuery] Int32 count)
	{
		return productsService.GetSettlements(page, count);
	}

	[HttpGet("products/get-by-id")]
	public Task<Settlements> GetProduct([FromQuery] Guid id)
	{
		return productsService.GetSettlement(id);
	}

	[HttpGet("products/remove")]
	public Task<Result> RemoveProduct([FromQuery] Guid id)
	{
		return productsService.RemoveSettlement(id);
	}
}
