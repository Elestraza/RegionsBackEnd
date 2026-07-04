using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;

namespace Goods.Services.Products;

public class SettlementsService(ISettlementsRepository repository) : ISettlementsService
{
    public async Task<Result> SaveSettlement(SettlementsBlank productBlank)
    {
        DataResult<Settlements> validationResult = await ValidateSettlementBlank(productBlank);
        if (validationResult.IsFail(out Settlements product)) return validationResult.ToResult();

        await repository.SaveSettlement(product);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<Product>> ValidateProductBlank(ProductBlank productBlank)
    {
        DataResult<Guid?> existProductValidationResult = await ValidateExistProduct(productBlank);
        if (existProductValidationResult.IsFail(out Guid? id)) return DataResult<Product>.Fail(existProductValidationResult);

        DataResult<ProductCategory> categoryValidationResult = ValidateProductCategory(productBlank);
        if (categoryValidationResult.IsFail(out ProductCategory category)) return DataResult<Product>.Fail(categoryValidationResult);

        DataResult<String> nameValidationResult = await ValidateProductName(productBlank);
        if (nameValidationResult.IsFail(out String name)) return DataResult<Product>.Fail(nameValidationResult);

        DataResult<Decimal> priceValidationResult = ValidateProductPrice(productBlank);
        if (priceValidationResult.IsFail(out Decimal price)) return DataResult<Product>.Fail(priceValidationResult);

        Product product = new(
            id ?? Guid.NewGuid(),
            category,
            name,
            productBlank.Description,
            price,
            isRemoved: false
        );

        return DataResult<Product>.Success(product);
    }

    private async Task<DataResult<Guid?>> ValidateExistProduct(ProductBlank productBlank)
    {
        if (productBlank.Id is not { } id)
            return DataResult<Guid?>.Success(null);

        Product existProduct = await GetProduct(id);
        if (existProduct.IsRemoved) return DataResult<Guid?>.Fail("Продукт удален");

        return DataResult<Guid?>.Success(id);
    }

    private DataResult<ProductCategory> ValidateProductCategory(ProductBlank productBlank)
    {
        if (productBlank.Category is not { } category)
            return DataResult<ProductCategory>.Fail("Выберите категорию продукта");

        if (!Enum.IsDefined(category))
            throw new Exception($"Категория {category} не существует");

        return DataResult<ProductCategory>.Success(category);
    }

    private async Task<DataResult<String>> ValidateProductName(ProductBlank productBlank)
    {
        if (String.IsNullOrWhiteSpace(productBlank.Name))
            return DataResult<String>.Fail("Не указано название продукта");

        const Int32 maxProductNameLength = 255;
        if (productBlank.Name.Length >= maxProductNameLength)
            return DataResult<String>.Fail($"Название продукта слишком длинное. Максимально допустимо {maxProductNameLength} символов");

        Product? productWithSameName = await GetProduct(productBlank.Name);
        if (productWithSameName is not null && productWithSameName.Id != productBlank.Id)
            return DataResult<String>.Fail("Продукт с таким названием уже существует");

        return DataResult<String>.Success(productBlank.Name);
    }

    private DataResult<Decimal> ValidateProductPrice(ProductBlank productBlank)
    {
        if (productBlank.Price is not { } price)
            return DataResult<Decimal>.Fail("Не указана стоимость продукта");

        if (price < 0)
            return DataResult<Decimal>.Fail("Указана некорректная стоимость продукта");

        return DataResult<Decimal>.Success(price);
    }
    
    #endregion Validation

    public async Task<Settlements> GetSettlement(Int32 id)
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

    public async Task<Result> RemoveSettlement(Int32 id)
    {
        Settlements settlement = await GetSettlement(id);
        if (settlement.IsRemoved) return Result.Fail("Продукт уже удален");

        await repository.RemoveSettlement(id);

        return Result.Success();
    }
}
