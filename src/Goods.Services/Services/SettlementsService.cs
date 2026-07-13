using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;
using System.Xml.Linq;

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
        if (typeValidationResult.IsFail(out SettlementsTypes settlementtype)) return DataResult<Settlements>.Fail(typeValidationResult);

        DataResult<String> nameValidationResult = await ValidateSettlementName(blank);
        if (nameValidationResult.IsFail(out String name)) return DataResult<Settlements>.Fail(nameValidationResult);

        DataResult<Int32> populationValidationResult = ValidateSettlementPopulation(blank);
        if (populationValidationResult.IsFail(out Int32 population)) return DataResult<Settlements>.Fail(populationValidationResult);


        DataResult<Regions> regionValidationResult = await ValidateSettlementRegion(blank);
        if (regionValidationResult.IsFail(out Regions region)) return DataResult<Settlements>.Fail(regionValidationResult);

        DataResult<Int32> foundationDateValidationResult = ValidateSettlementFoundationDate(blank);
        if (foundationDateValidationResult.IsFail(out Int32 foundationyear)) return DataResult<Settlements>.Fail(foundationDateValidationResult);

        DataResult<Boolean> heroStatusValidationResult = ValidateSettlementHeroStatus(blank);
        if (heroStatusValidationResult.IsFail(out Boolean isHero)) return DataResult<Settlements>.Fail(heroStatusValidationResult);

        DataResult<Int32> hotelCostValidationResult = await ValidateSettlementAvgHotelCost(blank);
        if (hotelCostValidationResult.IsFail(out Int32 averageHotelCost)) return DataResult<Settlements>.Fail(hotelCostValidationResult);

        Settlements settlement = new(
            id ?? Guid.NewGuid(),
            settlementtype,
            name,
            population,
            region,
            foundationyear,
            isHero,
            averageHotelCost
        );
        Console.WriteLine(settlement.IsHero);
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
        if (blank.Type is not { } settlementtype)
            return DataResult<SettlementsTypes>.Fail("Выберите тип населенного пункта");

        if (!Enum.IsDefined(settlementtype))
            throw new Exception($"Категория {settlementtype} не существует");
        return DataResult<SettlementsTypes>.Success(settlementtype);
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

    private async Task<DataResult<Regions>> ValidateSettlementRegion(SettlementsBlank blank)
    {
        if (!(blank.Region is { } region))
            return DataResult<Regions>.Fail("Не указан регион");

        Regions selectedRegion = await regionsService.GetRegion(region.Id);

        if (blank.Region != null && selectedRegion.Id != blank.Region.Id)
            throw new Exception($"Региона {selectedRegion} не существует");

        

        return DataResult<Regions>.Success(selectedRegion);
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
        if ((blank.Type != SettlementsTypes.City) && (blank.IsHero == true))
        {
            blank.IsHero = false;
            return DataResult<Boolean>.Fail("Данный тип населенного пункта не может иметь статус Города-Героя");
        }
        Console.WriteLine(blank.IsHero);
        return DataResult<Boolean>.Success(blank.IsHero);
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
    public async Task<String> GetSettlementHistoryValue(Guid id)
    {
        Settlements? settlement = await repository.GetSettlement(id);
        if (DateTime.Now.Year - settlement.FoundationYear < settlement.Region.FederalRegion.HistoricalValueAge && settlement.IsHero == false && settlement.Type.CanHaveHistoricalValue())
        {
            return $"Населенный пункт {settlement.Name} не имеет историческую ценность";
        }
        return $"Населенный пункт {settlement.Name} имеет историческую ценность";
    }



    /*
        Каждый год возраста - +1 
        Население - каждый человек даёт 0.000001 (settlement.population * 0.000001)
        Коэффицент - вычисляется суммарно
        
        Тип:
            Город - 1.3
            ПГТ - 1.0
            Деревня - 0.7
            Село - 0.5
        Город-Герой - итоговый коэффицент x2
        Если нет отеля - делить итоговый коэффицент на 3

        settlementtype int NOT NULL,
        name varchar NOT NULL,
        population int NOT NULL,
	    region uuid NOT NULL,
        foundationyear int NOT NULL,
	    ishero bool NOT NULL DEFAULT False,
	    averagehotelcost int DEFAULT NULL,

        select * from settlements
        join regions on regions.id = settlements.region
        

     */
    public async Task<(Double, Settlements)[]> GetTopTenSettlements(Guid chosenRegion, DateOnly vacationDate)
    {
        // region.id заранее известен от пользователя

        Page<Settlements> settlements = await repository.GetSettlementsByRegion(chosenRegion);

        Double settlementScore = 0;
        (Double, Settlements) [] scoredSettlements = { };
        foreach (var settlement in settlements.Values.Where(s => s.Region.Id == chosenRegion))
        {
            settlementScore += (settlement.FoundationYear - vacationDate.Year);
            settlementScore += (settlement.Population * 0.000001);
            settlementScore += settlement.Type.ScorePoints();
            if (settlement.IsHero)
                settlementScore *= 2;
            if (settlement.AverageHotelCost == null)
                settlementScore /= 3;

            (Double, Settlements) st = (settlementScore, settlement);
            scoredSettlements.Append(st);
        }

        return scoredSettlements.OrderBy(i => i.Item1).ToArray();
    }


}
