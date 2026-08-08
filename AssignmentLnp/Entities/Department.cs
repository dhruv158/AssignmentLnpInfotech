namespace AssignmentLnp.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Employee> Employees { get; set; }
            = new List<Employee>();
    }
}
