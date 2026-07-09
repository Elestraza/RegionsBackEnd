using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;

namespace Goods.Services.Products;

public class FederalRegionsService(IFederalRegionsRepository repository) : IFederalRegionsService
{
    public async Task<Result> SaveFederalRegion(FederalRegionsBlank blank)
    {
        DataResult<FederalRegions> validationResult = await ValidateFederalRegions(blank);
        if (validationResult.IsFail(out FederalRegions federalRegion)) return validationResult.ToResult();

        await repository.SaveFederalRegion(federalRegion);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<FederalRegions>> ValidateFederalRegions(FederalRegionsBlank blank)
    {
        DataResult<Guid?> existValidationResult = await ValidateExistFederalRegion(blank);
        if (existValidationResult.IsFail(out Guid? id)) return DataResult<FederalRegions>.Fail(existValidationResult);

        DataResult<String> nameValidationResult = ValidateFederalRegionName(blank);
        if (nameValidationResult.IsFail(out String name)) return DataResult<FederalRegions>.Fail(nameValidationResult);

        DataResult<Int32> historicalValueAgeValidationResult = await ValidateFederalRegionHistoryAge(blank);
        if (historicalValueAgeValidationResult.IsFail(out Int32 historicalValueAge)) return DataResult<FederalRegions>.Fail(historicalValueAgeValidationResult);

        FederalRegions federalRegions = new(
            id ?? Guid.NewGuid(),
            name,
            historicalValueAge
        );

        return DataResult<FederalRegions>.Success(federalRegions);
    }

    private async Task<DataResult<Guid?>> ValidateExistFederalRegion(FederalRegionsBlank blank)
    {
        if (!(blank.Id is { } id))
            return DataResult<Guid?>.Success(null);

        try
        {
            FederalRegions exist = await GetFederalRegion(id);
        } catch
        {
                return DataResult<Guid?>.Fail("Продукт удален");
        }

        return DataResult<Guid?>.Success(id);
    }

    private async Task<DataResult<Int32>> ValidateFederalRegionHistoryAge(FederalRegionsBlank blank)
    {
        if (!(blank.HistoricalValueAge is { } age))
            return DataResult<Int32>.Fail("Введите требуемый возраст");

        return DataResult<Int32>.Success(age);
    }

    private DataResult<String> ValidateFederalRegionName(FederalRegionsBlank blank)
    {
        if (!(blank.Name is { } name))
            return DataResult<String>.Fail("Не указан автомобильный код");

        if (name.Length < 2)
            return DataResult<String>.Fail("Указан некорректный автомобильный код");

        return DataResult<String>.Success(name);
    }
    
    #endregion Validation

    public async Task<FederalRegions> GetFederalRegion(Guid Id)
    {
        FederalRegions? federalRegions = await repository.GetFederalRegion(Id);
        if (federalRegions is null) throw new Exception($"Автомобильный код {Id} не найден");

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

    public async Task<Result> RemoveFederalRegion(Guid Id)
    {
        try
        {
            FederalRegions federalRegions = await GetFederalRegion(Id);
        } catch
        {
            return Result.Fail("Автомобильный код уже удален");
        }
        await repository.RemoveFederalRegion(Id);

        return Result.Success();
    }
}
