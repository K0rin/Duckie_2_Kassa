using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

[Table("Транспорт")]
public class LegacyVehicle
{
    [Key]
    [Column("Номер", TypeName = "NCHAR")]
    [StringLength(10)]
    public string Licence { get; set; }

    [Column("Категория", TypeName = "NCHAR")]
    [StringLength(1)]
    public string PriceCategory { get; set; }

    [Column("Скидка", TypeName = "DECIMAL(2,2)")]
    public decimal Discount { get; set; }

    [Column("КодФирмы")] public int CompanyId { get; set; }
    public Guid new_id { get; set; }
}