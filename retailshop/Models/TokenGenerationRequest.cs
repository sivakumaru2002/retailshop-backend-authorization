namespace retailshop.Models
{
    public class CustomClaims
    {
        public bool Admin;
    }
    public class TokenGenerationRequest
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public required CustomClaims CustomClaims { get; set; }
    }
}
