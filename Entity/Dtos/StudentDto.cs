using StudentManagementApp.Enums;

namespace StudentManagementApp.Entity.Dtos
{
    public class StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Primary Primary { get; set; }
        public Secondary Secondary { get; set; }
        public string ClassName { get; set; }
        public bool IsPrimary { get; set; }
    }
}
