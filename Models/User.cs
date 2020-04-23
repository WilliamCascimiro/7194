using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models{
  [Table("Usuario")]
  public class User{

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(3, ErrorMessage="Este campo deve ter entre 3 e 20 caracteres")]
    [MaxLength(20, ErrorMessage="Este campo deve ter entre 3 e 20 caracteres")]
    public string Username { get; set; }
    
    [Required]
    [MinLength(3, ErrorMessage="Este campo deve ter entre 3 e 20 caracteres")]
    [MaxLength(20, ErrorMessage="Este campo deve ter entre 3 e 20 caracteres")]
    public string Password { get; set; }
    public string Role { get; set; }

  }
}