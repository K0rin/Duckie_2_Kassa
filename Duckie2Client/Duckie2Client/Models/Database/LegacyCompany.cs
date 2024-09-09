using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

[Table("Фирмы")]
public class LegacyCompany
{
    [Key] [Column("Код")] public int CompanyId { get; set; }

    private const string NO_DATA_LABEL = "Н/Д";

    [Column("Наименование", TypeName = "VARCHAR")]
    [StringLength(50)]
    public string Name { get; set; } = NO_DATA_LABEL;

    [Column("Адрес", TypeName = "NCHAR")]
    [StringLength(255)]
    public string Address { get; set; }
}