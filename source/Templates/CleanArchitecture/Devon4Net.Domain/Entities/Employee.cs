using System.ComponentModel.DataAnnotations;

namespace Devon4Net.Domain.Entities;

/// <summary>
/// Entity class for Employee
/// </summary>
public class Employee
{
    public Employee(string name, string surname, string mail)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Mail = mail;
    }

    /// <summary>
    /// Id
    /// </summary>
    [Key]
    public Guid Id { get; private set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Surname
    /// </summary>
    public string Surname { get; private set; }

    /// <summary>
    /// mail
    /// </summary>
    public string Mail { get; private set; }
}