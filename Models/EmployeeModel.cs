using System.ComponentModel.DataAnnotations;

public class EmployeeModel
{
    [Key]
    public int EmployeeId { get; set; }

    public string Name { get; set; }
    public string CNIC { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
    public string Designation { get; set; }
    public bool IsActive { get; set; } = true;

    public string IdentityUserId { get; set; }
}
