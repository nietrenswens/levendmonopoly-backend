using LevendMonopoly.Api.Controllers;

namespace LevendMonopoly.Api.InputValidation
{
    public static class TeamValidation
    {
        public static bool IsValidTeam(TeamPostBody team)
        {
            if (team.Name.Length >= 3 || team.Name.Length <= 50)
            {
                if (team.Password.Length >= 8 || team.Password.Length <= 50)
                {
                    return true;
                }
            }

            return false;
        }
    }
}