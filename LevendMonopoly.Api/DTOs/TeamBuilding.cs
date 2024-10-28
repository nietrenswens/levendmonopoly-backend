using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.DTOs
{
    public record TeamBuilding
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Price { get; init; }
        public string? Image { get; init; }
    }
}
