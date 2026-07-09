using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;

namespace Goods.Services.Products;

public class CarCodesService(ICarCodesRepository repository, IRegionsService regionsService) : ICarCodesService
{
    public async Task<Result> SaveCarCode(CarCodesBlank blank)
    {
        DataResult<CarCodes> validationResult = await ValidateCarCodes(blank);
        if (validationResult.IsFail(out CarCodes product)) return validationResult.ToResult();

        await repository.SaveCarCode(product);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<CarCodes>> ValidateCarCodes(CarCodesBlank blank)
    {
        DataResult<Guid?> existValidationResult = await ValidateExistCarCodes(blank);
        if (existValidationResult.IsFail(out Guid? id)) return DataResult<CarCodes>.Fail(existValidationResult);

        DataResult<Regions> regionValidationResult = await ValidateCarCodeRegion(blank);
        if (regionValidationResult.IsFail(out Regions regions)) return DataResult<CarCodes>.Fail(regionValidationResult);

        DataResult<String> codeValidationResult = ValidateCarCode(blank);
        if (codeValidationResult.IsFail(out String code)) return DataResult<CarCodes>.Fail(codeValidationResult);

        CarCodes carCode = new(
            id ?? Guid.NewGuid(),
            code,
            regions
        );

        return DataResult<CarCodes>.Success(carCode);
    }

    private async Task<DataResult<Guid?>> ValidateExistCarCodes(CarCodesBlank blank)
    {
        if (!(blank.Id is { } id))
            return DataResult<Guid?>.Success(null);

        try
        {
            CarCodes existProduct = await GetCarCode(id);
        } catch
        {
                return DataResult<Guid?>.Fail("Продукт удален");
        }

        return DataResult<Guid?>.Success(id);
    }

    private async Task<DataResult<Regions>> ValidateCarCodeRegion(CarCodesBlank blank)
    {
        if (!(blank.Regions is { } region))
            return DataResult<Regions>.Fail("Выберите категорию продукта");
        Regions selectedRegion = await regionsService.GetRegion(region.Id);


        if (selectedRegion is not null && selectedRegion.Id != blank.Regions.Id)
            throw new Exception($"Региона {selectedRegion} не существует");

        return DataResult<Regions>.Success(selectedRegion);
    }

    private DataResult<String> ValidateCarCode(CarCodesBlank blank)
    {
        if (!(blank.Code is { } code))
            return DataResult<String>.Fail("Не указан автомобильный код");

        if (code.Length < 2)
            return DataResult<String>.Fail("Указан некорректный автомобильный код");

        return DataResult<String>.Success(code);
    }
    
    #endregion Validation

    public async Task<CarCodes> GetCarCode(Guid Id)
    {
        CarCodes? carCodes = await repository.GetCarCode(Id);
        if (carCodes is null) throw new Exception($"Автомобильный код {Id} не найден");

        return carCodes;
    }

    private Task<CarCodes?> GetCarCode(String name)
    {
        return repository.GetCarCode(name);
    }

    public Task<Page<CarCodes>> GetCarCodes(Int32 page, Int32 countInPage)
    {
        return repository.GetCarCodes(page, countInPage);
    }

    public async Task<Result> RemoveCarCode(Guid Id)
    {
        try
        {
            CarCodes carCodes = await GetCarCode(Id);
        } catch
        {
            return Result.Fail("Автомобильный код уже удален");
        }
        await repository.RemoveCarCode(Id);

        return Result.Success();
    }
}
