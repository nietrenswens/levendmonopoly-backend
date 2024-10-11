using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.DTOs
{
    public class SessionDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }

        public SessionDTO(Guid id, Guid userId, string token, bool isActive, DateTime expirationDate)
        {
            Id = id;
            UserId = userId;
            Token = token;
            IsActive = isActive;
            ExpirationDate = expirationDate;
        }

        public SessionDTO(Session session) : this(session.Id, session.UserId, session.Token, session.IsActive, session.ExpirationDate) { }
    }
}
