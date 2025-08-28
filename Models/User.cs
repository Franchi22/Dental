namespace DentalMVP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Recepcion;
        // MVP: sin password (solo Admin demo). Luego: hash + login.
    }
}