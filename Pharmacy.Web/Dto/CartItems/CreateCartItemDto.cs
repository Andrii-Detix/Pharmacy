namespace Pharmacy.Web.Dto.CartItems;

public record CreateCartItemDto(Guid ProductId, int Quantity);