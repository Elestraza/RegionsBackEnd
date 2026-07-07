using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;

namespace Goods.Services.Products;

public class SettlementsTypesService(ISettlementsTypesRepository repository) : ISettlementsTypesService
{
    public async Task<Result> SaveSettlementsType(SettlementsTypesBlank blank)
    {
        DataResult<SettlementsTypes> validationResult = await ValidateSettlementsTypesBlank(blank);
        if (validationResult.IsFail(out SettlementsTypes product)) return validationResult.ToResult();

        await repository.SaveSettlementsType(product);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<SettlementsTypes>> ValidateSettlementsTypesBlank(SettlementsTypesBlank blank)
    {
        DataResult<Guid?> existProductValidationResult = await ValidateExistSettlementsTypes(blank);
        if (existProductValidationResult.IsFail(out Guid? id)) return DataResult<SettlementsTypes>.Fail(existProductValidationResult);

        DataResult<String> nameValidationResult = await ValidateSettlementsTypes(blank);
        if (nameValidationResult.IsFail(out String type)) return DataResult<SettlementsTypes>.Fail(nameValidationResult);

        SettlementsTypes settlementsType = new(
            id ?? Guid.NewGuid(),
            type
        );

        return DataResult<SettlementsTypes>.Success(settlementsType);
    }

    private async Task<DataResult<Guid?>> ValidateExistSettlementsTypes(SettlementsTypesBlank blank)
    {
        if (!(blank.Id is { } id))
            return DataResult<Guid?>.Success(null);
        Console.WriteLine($"{id} | {blank.Id}");
        try
        {
            SettlementsTypes existSettlementType = await GetSettlementsType(id);
        } catch
        {
            return DataResult<Guid?>.Fail("Тип населенного пункта удален");
        }
        return DataResult<Guid?>.Success(id);
    }

    private async Task<DataResult<String>> ValidateSettlementsTypes(SettlementsTypesBlank blank)
    {
        if (blank.Type is not { } type)
            return DataResult<String>.Fail("Не указан тип населенного пункта");

        //SettlementsTypes? productWithSameName = await GetSettlementsType(blank.Type);
        //if (productWithSameName is not null && productWithSameName.Id != blank.Id)
        //    return DataResult<String>.Fail("Продукт с таким названием уже существует");

        //if (!Enum.IsDefined(type, blank.Type))
        //    throw new Exception($"Категория {type} не существует");

        return DataResult<String>.Success(type);
    }
    #endregion Validation

    public async Task<SettlementsTypes?> GetSettlementsType(Guid id)
    {
        SettlementsTypes? settlementsType = await repository.GetSettlementsType(id);
        Console.WriteLine(id);
        if (settlementsType is null) throw new Exception($"Населенный пункт {id} не найден");

        return settlementsType;
    }

    private Task<SettlementsTypes?> GetSettlementsType(String type)
    {
        return repository.GetSettlementsType(type);
    }

    public Task<Page<SettlementsTypes>> GetSettlementsTypes(Int32 page, Int32 countInPage)
    {
        return repository.GetSettlementsTypes(page, countInPage);
    }

    public async Task<Result> RemoveSettlementsType(Guid id)
    {
        try
        {
            SettlementsTypes settlementsType = await GetSettlementsType(id);
        }
        catch
        {
            return Result.Fail("Тип населенного пункта уже удален");
        }
        await repository.RemoveSettlementsType(id);

        return Result.Success();
    }
}
