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
    public string KeepImg { get; set; }
    public int Shares { get; set; }
    public int Views { get; set; }
    public bool IsPrivate { get; set; }

    public Keep() { }
  }
}