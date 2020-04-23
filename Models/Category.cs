using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models{
  
  [Table("Categoria")]
  public class Category{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage="Este campo é obrigatório")]
    [MinLength(3, ErrorMessage="Este campo deve ter entre 3 e 60 caracteres")]
    [MaxLength(60, ErrorMessage="Este campo deve ter entre 3 e 60 caracteres")]
    public string Title { get; set; }

  }
}