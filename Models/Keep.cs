using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Keep
  {
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    public string Description { get; set; }
    public string Img { get; set; }
    public int Shares { get; set; }
    public int Views { get; set; }
    public int Keeps { get; set; }
    public bool IsPrivate { get; set; } = true;
    public int Id { get; set; }
    public string UserId { get; set; }

    public Keep() { }
  }
}