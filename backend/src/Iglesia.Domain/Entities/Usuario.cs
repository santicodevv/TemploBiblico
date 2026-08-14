using Microsoft.AspNetCore.Identity;

namespace Iglesia.Domain.Entities;

public class Usuario : IdentityUser
{
  public string NombreCompleto {get; set;} = string.Empty;
   public int? MinisterioId {get; set;}

     public string? RefreshToken {get; set;}

   public DateTime? RefreshTokenExpiryTime {get; set;}
}