using System;
using System.ComponentModel.DataAnnotations;

namespace Duckie2Client.Models.Database;

/// <summary>
/// <para>
/// Represents itself as an entity of type “User.”
/// </para>
/// <para>
/// Contains information about the user of the Application.
/// </para>
/// </summary>
public class User
{
    /// <summary>
    /// User identifier. 
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    ///User login for signing in to the system. 
    /// </summary>
    [Required]
    public string Login { get; set; }

    /// <summary>
    ///A hash of the user’s login password. 
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Password { get; set; }

    /// <summary>
    ///Firstname of the user. 
    /// </summary>
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    ///Last name of the user. 
    /// </summary>
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    /// <summary>
    ///A list of means of communication with an Operator. 
    /// </summary>
    public CommunicationMean? Communication { get; set; }

    /// <summary>
    ///User role indicator: true - Manager, false - Operator. 
    /// </summary>
    [Required]
    public bool IsStaff { get; set; }

    /// <summary>
    ///User registration date. 
    /// </summary>
    public DateOnly RegistrationDate { get; set; }

    /// <summary>
    ///Operator wage rate. 
    /// </summary>
    public Rate? SalaryRate { get; set; }
}