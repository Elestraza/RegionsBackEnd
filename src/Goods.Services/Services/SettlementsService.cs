using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;

namespace Goods.Services.Products;

public class SettlementsService(ISettlementsRepository repository, IRegionsService regionsService) : ISettlementsService
{
    public async Task<Result> SaveSettlement(SettlementsBlank blank)
    {
        DataResult<Settlements> validationResult = await ValidateSettlementBlank(blank);
        if (validationResult.IsFail(out Settlements product)) return validationResult.ToResult();

        await repository.SaveSettlement(product);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<Settlements>> ValidateSettlementBlank(SettlementsBlank blank)
    {
        DataResult<Guid?> existValidationResult = await ValidateExistSettlement(blank);
        if (existValidationResult.IsFail(out Guid? id)) return DataResult<Settlements>.Fail(existValidationResult);

        DataResult<SettlementsTypes> typeValidationResult = await ValidateSettlementType(blank);
        if (typeValidationResult.IsFail(out SettlementsTypes type)) return DataResult<Settlements>.Fail(typeValidationResult);

        DataResult<String> nameValidationResult = await ValidateSettlementName(blank);
        if (nameValidationResult.IsFail(out String name)) return DataResult<Settlements>.Fail(nameValidationResult);

        DataResult<Int32> populationValidationResult = ValidateSettlementPopulation(blank);
        if (populationValidationResult.IsFail(out Int32 population)) return DataResult<Settlements>.Fail(populationValidationResult);


        DataResult<Guid> regionValidationResult = await ValidateSettlementRegion(blank);
        if (regionValidationResult.IsFail(out Guid region)) return DataResult<Settlements>.Fail(regionValidationResult);

        DataResult<Int32> foundationDateValidationResult = ValidateSettlementFoundationDate(blank);
        if (foundationDateValidationResult.IsFail(out Int32 foundationyear)) return DataResult<Settlements>.Fail(foundationDateValidationResult);

        DataResult<Boolean> heroStatusValidationResult = ValidateSettlementHeroStatus(blank);
        if (heroStatusValidationResult.IsFail(out Boolean isHero)) return DataResult<Settlements>.Fail(heroStatusValidationResult);

        DataResult<Int32> hotelCostValidationResult = await ValidateSettlementAvgHotelCost(blank);
        if (hotelCostValidationResult.IsFail(out Int32 averageHotelCost)) return DataResult<Settlements>.Fail(hotelCostValidationResult);

        Settlements settlement = new(
            id ?? Guid.NewGuid(),
            type,
            name,
            population,
            region,
            foundationyear,
            isHero,
            averageHotelCost
        );

        return DataResult<Settlements>.Success(settlement);
    }

    private async Task<DataResult<Guid?>> ValidateExistSettlement(SettlementsBlank blank)
    {
        if (!(blank.Id is { } id))
            return DataResult<Guid?>.Success(null);

        try
        {
            Settlements existProduct = await GetSettlement(id);
        } catch
        {
            return DataResult<Guid?>.Fail("Населенный пункт удален");
        }
        return DataResult<Guid?>.Success(id);
    }

    private async Task<DataResult<SettlementsTypes>> ValidateSettlementType(SettlementsBlank blank)
    {
        if (blank.Type is not { } type)
            return DataResult<SettlementsTypes>.Fail("Выберите тип населенного пункта");

        if (!Enum.IsDefined(type))
            throw new Exception($"Категория {type} не существует");
        return DataResult<SettlementsTypes>.Success(type);
    }

    private async Task<DataResult<String>> ValidateSettlementName(SettlementsBlank blank)
    {
        if (String.IsNullOrWhiteSpace(blank.Name))
            return DataResult<String>.Fail("Не указано название населенного пункта");

        const Int32 maxSettlementNameLength = 255;
        if (blank.Name.Length >= maxSettlementNameLength)
            return DataResult<String>.Fail($"Название населенного пункта слишком длинное. Максимально допустимо {maxSettlementNameLength} символов");

        Settlements? productWithSameName = await GetSettlement(blank.Name);
        if (productWithSameName is not null && productWithSameName.Id != blank.Id)
            return DataResult<String>.Fail("Населенный пункт с таким названием уже существует");

        return DataResult<String>.Success(blank.Name);
    }

    private DataResult<Int32> ValidateSettlementPopulation(SettlementsBlank blank)
    {
        if (!(blank.Population is { } population))
            return DataResult<Int32>.Fail("Не указан автомобильный код");

        if (population < 0)
            return DataResult<Int32>.Fail("Указан некорректный автомобильный код");

        return DataResult<Int32>.Success(population);
    }

    private async Task<DataResult<Guid>> ValidateSettlementRegion(SettlementsBlank blank)
    {
        if (!(blank.Region is { } region))
            return DataResult<Guid>.Fail("Не указан регион");

        Regions selectedRegion = await regionsService.GetRegion(region);

        if (blank.Region != null && selectedRegion.Id != blank.Id)
            throw new Exception($"Региона {selectedRegion} не существует");

        

        return DataResult<Guid>.Success(selectedRegion.Id);
    }

    private async Task<DataResult<Int32>> ValidateSettlementAvgHotelCost(SettlementsBlank blank)
    {
        if (!(blank.AverageHotelCost is { } averageHotelCost))
            return DataResult<Int32>.Fail("Не указан автомобильный код");

        if (averageHotelCost < 0)
            return DataResult<Int32>.Fail("Указан некорректный автомобильный код");

        return DataResult<Int32>.Success(averageHotelCost);
    }

    private DataResult<Boolean> ValidateSettlementHeroStatus(SettlementsBlank blank)
    {
        throw new NotImplementedException();
    }

    private DataResult<Int32> ValidateSettlementFoundationDate(SettlementsBlank blank)
    {
        if (!(blank.FoundationYear is { } foundationYear))
            return DataResult<Int32>.Fail("Не указан год");

        if (foundationYear < 0)
            return DataResult<Int32>.Fail("Указан некорректный год");

        return DataResult<Int32>.Success(foundationYear);
    }

    #endregion Validation

    public async Task<Settlements> GetSettlement(Guid id)
    {
        Settlements? settlement = await repository.GetSettlement(id);
        if (settlement is null) throw new Exception($"Населенный пункт {id} не найден");

        return settlement;
    }

    private Task<Settlements?> GetSettlement(String name)
    {
        return repository.GetSettlement(name);
    }

    public Task<Page<Settlements>> GetSettlements(Int32 page, Int32 countInPage)
    {
        return repository.GetSettlements(page, countInPage);
    }

    public async Task<Result> RemoveSettlement(Guid id)
    {
        try
        {
            Settlements settlement = await GetSettlement(id);
        } catch
        {
            return Result.Fail("Населенный пункт уже удален");
        }
        await repository.RemoveSettlement(id);

        return Result.Success();
    }
}
