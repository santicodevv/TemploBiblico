using Microsoft.AspNetCore.Identity;

namespace Iglesia.Domain.Entities;

public class Usuario : IdetityUser
{
  public string NombreCompleto {get; set;} = string.Empty;
   public int? MinisterioId {get; set;};

     public string? RefreshToke {get; set;}

   public DateTime? RefreshToke {get; set;}
}