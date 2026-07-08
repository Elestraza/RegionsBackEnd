using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Services.Products.Repositories.Models;
using Goods.Tools.Types.Results;

namespace Goods.Services.Products;

public class RegionsService(IRegionsRepository repository) : IRegionsService
{
    public async Task<Result> SaveRegion(RegionsBlank blank)
    {
        DataResult<Regions> validationResult = await ValidateRegionsBlank(blank);
        if (validationResult.IsFail(out Regions product)) return validationResult.ToResult();

        await repository.SaveRegion(product);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<Regions>> ValidateRegionsBlank(RegionsBlank blank)
    {
        DataResult<Guid?> existProductValidationResult = await ValidateExistRegion(blank);
        if (existProductValidationResult.IsFail(out Guid? regionId)) return DataResult<Regions>.Fail(existProductValidationResult);

        DataResult<String> nameValidationResult = await ValidateRegionName(blank);
        if (nameValidationResult.IsFail(out String name)) return DataResult<Regions>.Fail(nameValidationResult);

        DataResult<FederalRegions> categoryValidationResult = await ValidateFederalRegion(blank);
        if (categoryValidationResult.IsFail(out FederalRegions federalRegion)) return DataResult<Regions>.Fail(categoryValidationResult);

        Regions region = new(
            regionId ?? Guid.NewGuid(),
            name,
            federalRegion
        );

        return DataResult<Regions>.Success(region);
    }

    private async Task<DataResult<Guid?>> ValidateExistRegion(RegionsBlank blank)
    {
        if (!(blank.Id is { } id))
            return DataResult<Guid?>.Success(null);
        try
        {
            Regions existProduct = await GetRegion(id);
        } catch
        {
                return DataResult<Guid?>.Fail("Продукт удален");
        }
        return DataResult<Guid?>.Success(id);
    }

    private async Task<DataResult<FederalRegions>> ValidateFederalRegion(RegionsBlank blank)
    {

        if (blank.FederalRegion is not { } federalRegion)
            return DataResult<FederalRegions>.Fail("Выберите регион");

        if(!Enum.IsDefined(federalRegion))
            throw new Exception($"Категория {federalRegion} не существует");

        return DataResult<FederalRegions>.Success(federalRegion);
    }

    private async Task<DataResult<String>> ValidateRegionName(RegionsBlank blank)
    {
        if (String.IsNullOrWhiteSpace(blank.Name))
            return DataResult<String>.Fail("Не указано название региона");

        const Int32 maxNameLength = 255;
        if (blank.Name.Length >= maxNameLength)
            return DataResult<String>.Fail($"Название региона слишком длинное. Максимально допустимо {maxNameLength} символов");

        Regions? productWithSameName = await GetRegion(blank.Name);
        if (productWithSameName is not null && productWithSameName.Id != blank.Id)
            return DataResult<String>.Fail("Регион с таким названием уже существует");

        return DataResult<String>.Success(blank.Name);
    }

    #endregion Validation

    public async Task<Regions> GetRegion(Guid id)
    {
        Regions? product = await repository.GetRegion(id);
        if (product is null) throw new Exception($"Регион {id} не найден");

        return product;
    }

    private Task<Regions?> GetRegion(String name)
    {
        return repository.GetRegion(name);
    }

    public Task<Page<Regions>> GetRegions(Int32 page, Int32 countInPage)
    {
        return repository.GetRegions(page, countInPage);
    }

    public async Task<Result> RemoveRegion(Guid id)
    {
        try
        {
            Regions product = await GetRegion(id);
        } catch
        {
            return Result.Fail("Регион уже удален");
        }
        await repository.RemoveRegion(id);

        return Result.Success();
    }
}
