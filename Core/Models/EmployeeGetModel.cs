namespace Reto2eSge_3__.Core.Models
{
    public class EmployeeGetModel
    {
        public EmployeeGetModel() { }

        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
    }
}
