using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;

namespace Goods.Services.Products;

public class FederalRegionsService(IFederalRegionsRepository repository) : IFederalRegionsService
{
    public async Task<Result> SaveFederalRegion(FederalRegionsBlank blank)
    {
        DataResult<FederalRegions> validationResult = await ValidateFederalRegionsBlank(blank);
        if (validationResult.IsFail(out FederalRegions product)) return validationResult.ToResult();

        await repository.SaveFederalRegion(product);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<FederalRegions>> ValidateFederalRegionsBlank(FederalRegionsBlank blank)
    {
        DataResult<Guid?> existProductValidationResult = await ValidateExistProduct(blank);
        if (existProductValidationResult.IsFail(out Guid? id)) return DataResult<FederalRegions>.Fail(existProductValidationResult);

        DataResult<String> categoryValidationResult = await ValidateFederalRegionName(blank);
        if (categoryValidationResult.IsFail(out String name)) return DataResult<FederalRegions>.Fail(categoryValidationResult);
        FederalRegions federalRegion = new(
            id ?? Guid.NewGuid(),
            name
        );

        return DataResult<FederalRegions>.Success(federalRegion);
    }

    private async Task<DataResult<Guid?>> ValidateExistProduct(FederalRegionsBlank blank)
    {
        if (!(blank.Id is { } id))
            return DataResult<Guid?>.Success(null);
        try
        {
            FederalRegions existProduct = await GetFederalRegion(id);
        } catch
        {
            return DataResult<Guid?>.Fail("Региона не существует");
        }
        return DataResult<Guid?>.Success(id);
    }

    private async Task<DataResult<String>> ValidateFederalRegionName(FederalRegionsBlank blank)
    {
        if (String.IsNullOrWhiteSpace(blank.Name))
            return DataResult<String>.Fail("Не указано название продукта");

        const Int32 maxProductNameLength = 255;
        if (blank.Name.Length >= maxProductNameLength)
            return DataResult<String>.Fail($"Название продукта слишком длинное. Максимально допустимо {maxProductNameLength} символов");

        FederalRegions? productWithSameName = await GetFederalRegion(blank.Name);
        if (productWithSameName is not null && productWithSameName.Id != blank.Id)
            return DataResult<String>.Fail("Продукт с таким названием уже существует");

        return DataResult<String>.Success(blank.Name);
    }
    
    #endregion Validation

    public async Task<FederalRegions> GetFederalRegion(Guid id)
    {
        FederalRegions? federalRegions = await repository.GetFederalRegion(id);
        if (federalRegions is null) throw new Exception($"Продукт {id} не найден");

        return federalRegions;
    }

    private Task<FederalRegions?> GetFederalRegion(String name)
    {
        return repository.GetFederalRegion(name);
    }

    public Task<Page<FederalRegions>> GetFederalRegions(Int32 page, Int32 countInPage)
    {
        return repository.GetFederalRegions(page, countInPage);
    }

    public async Task<Result> RemoveFederalRegion(Guid id)
    {
        try
        {
            FederalRegions federalRegions = await GetFederalRegion(id);
        } catch
        {
                return Result.Fail("Регион уже удален");
        }
        await repository.RemoveFederalRegion(id);

        return Result.Success();
    }
}
