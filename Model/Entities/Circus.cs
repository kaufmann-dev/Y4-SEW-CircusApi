using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("CIRCUSES")]
public class Circus
{
    [Column("CIRCUS_ID")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NAME")]
    [Required, StringLength(50)]
    public string Name { get; set; }
}