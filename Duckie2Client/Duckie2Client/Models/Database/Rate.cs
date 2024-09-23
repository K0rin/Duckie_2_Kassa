using System;
using System.ComponentModel.DataAnnotations;

namespace Duckie2Client.Models.Database;

/// <summary>
/// <para>
/// Represents an entity of type “Rate.”
/// </para>
/// <para>
/// Contains information on wage rates, pollution levels, sales tax. Also includes information on the rate
/// validity period.
/// </para>
/// </summary>
public class Rate
{
    /// <summary>
    /// Rate identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// <para>
    /// Value of the rate.
    /// </para>
    /// <para>
    /// Valid values are numbers in the range of 1 to 99.
    /// </para>
    /// </summary>
    [Required]
    public int Value { get; set; }
    // todo: Проверка числа на принадлежность к диапазону при установке значения.
    // Выбрасывать исключение "Число вне диапазона допустимых значений".
    // Исключение критическое, завершает работу приложения.
    // todo: При получении данных, выдавать самую последнюю ставку. Чтобы не возиться со списком.

    /// <summary>
    /// Rate commencement date.
    /// </summary>
    [Required]
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Rate end date.
    /// </summary>
    [Required]
    public DateOnly EndDate { get; set; }
}