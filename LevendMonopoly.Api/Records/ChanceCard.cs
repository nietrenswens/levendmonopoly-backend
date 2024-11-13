namespace LevendMonopoly.Api.Records
{
    public record ChanceCard
    {
        public required string Prompt { get; init; }
        public required int Result { get; init; }
    }
}
