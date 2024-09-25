using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// <para>
/// Represents itself as an entity of type “User”.
/// </para>
/// <para>
/// Contains information about the user (Operators and Admin) of the Application.
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
    /// User login for signing in to the system. 
    /// </summary>
    [Required]
    [StringLength(25)]
    [Column(TypeName = "VARCHAR")]
    public required string Login { get; set; }

    /// <summary>
    /// A hash of the user’s login password. 
    /// </summary>
    [Required]
    [StringLength(64)]
    [Column(TypeName = "VARCHAR")]
    public required string Password { get; set; }

    /// <summary>
    /// Firstname of the user. 
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column(TypeName = "NVARCHAR")]
    public required string FirstName { get; set; }

    /// <summary>
    /// Last name of the user. 
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column(TypeName = "NVARCHAR")]
    public required string LastName { get; set; }

    /// <summary>
    /// A list of ways to communicate with an Operator.
    /// </summary>
    public List<CommunicationMean> Communication { get; set; }

    /// <summary>
    /// User role indicator: true - Manager, false - Operator. 
    /// </summary>
    [Required]
    public bool IsStaff { get; set; }

    /// <summary>
    /// User registration date. 
    /// </summary>
    public DateOnly RegistrationDate { get; set; }

    /// <summary>
    /// Operator wage rate. 
    /// </summary>
    public Rate? SalaryRate { get; set; }

    /// <summary>
    /// The branch of the company where the employee works.
    /// </summary>
    [Required]
    public required Branch? Branch { get; set; }
}