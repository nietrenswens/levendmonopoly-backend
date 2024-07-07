namespace LevendMonopoly.Api.Records
{
   public record UserLoginBody
   {
      public string Email { get; init; } = null!;
      public string Password { get; init; } = null!;
   }
}