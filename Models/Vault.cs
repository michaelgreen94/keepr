using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Vault
  {
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    public string Description { get; set; }
    [Required]
    [MaxLength(255)]
    public string UserId { get; set; }
    public int Id { get; set; }

    public Vault() { }
    public Vault(string name, string description, string userid)
    {
      Name = name;
      Description = description;
      UserId = userid;
    }
  }
}