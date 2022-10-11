using StudentManagementApp.Enums;

namespace StudentManagementApp.Entity
{
    public class Student : BaseEntity
    {
        public string AdmissionNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Primary Primary { get; set; }
        public Secondary Secondary { get; set; }
        public string ClassName { get; set; }
        public bool IsPrimary { get; set; }

        public override string ToString()
        {
            return $"{Id}\t{AdmissionNo}\t{FirstName}\t{LastName}\t{MiddleName}\t{DateOfBirth}\t{Gender}\t{ClassName}\t{IsPrimary}\t{CreatedAt}\t{UpdatedAt}";
        }

        public static Student ToStudent(string str)
        {
            var studentStr = str.Split("\t");

            var student = new Student
            {
                Id = int.Parse(studentStr[0]),
                AdmissionNo = studentStr[1],
                FirstName = studentStr[2],
                LastName = studentStr[3],
                MiddleName = studentStr[4],
                DateOfBirth = DateTime.Parse(studentStr[5]),
                Gender = Enum.Parse<Gender>(studentStr[6]),
                ClassName = studentStr[7],
                IsPrimary = bool.Parse(studentStr[8]),
                CreatedAt = DateTime.Parse(studentStr[9]),
                UpdatedAt = DateTime.Parse(studentStr[10]),
            };

            return student;
        }
    }
}
