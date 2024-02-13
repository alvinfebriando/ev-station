namespace astra_otoparts;
public enum Role
{
    Admin,
    User
}
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}
