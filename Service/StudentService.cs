using StudentManagementApp.Commons;
using StudentManagementApp.Entity;
using StudentManagementApp.Entity.Dtos;
using StudentManagementApp.Enums;
using StudentManagementApp.Repository;
using StudentManagementApp.Repository.Contracts;
using StudentManagementApp.Service.Contracts;

namespace StudentManagementApp.Service
{
    public class StudentService : IStudentService
    {
        private static IStudentRepository studentRepository;

        public StudentService()
        {
            studentRepository = new StudentRepository();
        }

        public void Create(StudentDto request)
        {
            try
            {
                var students = studentRepository.GetAllStudents();
                int id = (students.Count != 0) ? students[students.Count - 1].Id + 1 : 1;
                string code = Helper.GenerateCode(id);

                Console.Write("Enter student firstname: ");
                request.FirstName = Console.ReadLine();

                Console.Write("Enter student lastname: ");
                request.LastName = Console.ReadLine();

                Console.Write("Enter student middlename: ");
                request.MiddleName = Console.ReadLine();

                Console.Write("Enter student date of birth (yyyy/mm/dd): ");
                request.DateOfBirth = DateTime.Parse(Console.ReadLine());

                int gender = Helper.SelectEnum("Enter student gender:\nEnter 1 for Male\nEnter 2 for Female: ", 1, 2);
                request.Gender = (Gender)gender;

                Console.WriteLine("Are you enrolling for primary class?: ");
                request.IsPrimary = bool.Parse(Console.ReadLine());

                if (request.IsPrimary == false)
                {
                    int secondary = Helper.SelectEnum("Select class to enroll in:\nEnter 1 for JSS1\nEnter 2 for JSS2\nEnter 3 for JSS3\nEnter 4 for SSS1\nEnter 5 for SSS2\nEnter 6 for SSS3: ", 1, 6);
                    request.Secondary = (Secondary)secondary;
                    request.ClassName = request.Secondary.ToString();
                }
                else
                {
                    int primary = Helper.SelectEnum("Select class to enroll in:\nEnter 1 for Primary 1\nEnter 2 for Primary 2\nEnter 3 for Primary 3\nEnter 4 for Primary 4\nEnter 5 for Primary 5\nEnter 6 for Primary 6: ", 1, 6);
                    request.Primary = (Primary)primary;
                    request.ClassName = request.Primary.ToString();
                }

                var student = new Student
                {
                    Id = id,
                    AdmissionNo = code,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    IsPrimary = request.IsPrimary,
                    ClassName = request.ClassName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = new DateTime(0001, 01, 01)
                };

                var findStudent = studentRepository.GetByIdOrAdmissionNo(student.Id, student.AdmissionNo);

                if (findStudent == null)
                {
                    students.Add(student);
                    studentRepository.WriteToFile(student);
                    Console.WriteLine($"New student with admission number \"{student.AdmissionNo}\" created successfully!");
                }
                else
                {
                    Console.WriteLine($"Student with {student.AdmissionNo} already exist!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var student = studentRepository.GetById(id);
                var students = studentRepository.GetAllStudents();

                if (student == null)
                {
                    Console.WriteLine($"Student with the id: {id} not found");
                    return;
                }

                students.Remove(student);
                studentRepository.RefreshFile();
                Console.WriteLine($"Student with the id: {id} successfully deleted.");
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong:{ex.Message}");
            }
        }

        public void GetAll()
        {
            var students = studentRepository.GetAllStudents();

            foreach (var student in students)
            {
                PrintListView(student);
            }
        }

        public void GetAStudent(int id)
        {
            var student = studentRepository.GetById(id);

            if (student == null)
            {
                Console.WriteLine("Student does not exist!");
                return;
            }

            PrintDetailView(student);
        }

        public void PrintDetailView(Student student)
        {
            var dateCreated = student.CreatedAt.ToShortDateString();
            var dateModified = student.UpdatedAt != new DateTime(0001, 01, 01) ? student.UpdatedAt.ToShortDateString() : "Not modified yet";
            Console.WriteLine($"Code: {student.AdmissionNo}\nFullname: {student.LastName} {student.FirstName}\nClass: {student.ClassName}\nGender: {student.Gender}\nCreated: {dateCreated}\nModified: {dateModified}");
        }

        public void PrintListView(Student student)
        {
            var dateCreated = student.CreatedAt.ToShortDateString();
            string dateModified;

            if (student.UpdatedAt == new DateTime(0001, 01, 01))
            {
                dateModified = "Not modified yet";
            }
            else if (student.CreatedAt.ToShortDateString() == student.UpdatedAt.ToShortDateString())
            {
                dateModified = student.UpdatedAt.ToString("hh:mm tt");
            }
            else
            {
                dateModified = student.UpdatedAt.ToShortDateString();
            }

            Console.WriteLine($"Admission No: {student.AdmissionNo}\tFullname: {student.LastName} {student.FirstName}\tGender: {student.Gender}\tCreated: {dateCreated}\tModified: {dateModified}");
        }

        public void Update(int id, StudentDto request)
        {
            var student = studentRepository.GetById(id);

            if (student != null)
            {
                Console.Write("Enter student firstname: ");
                request.FirstName = Console.ReadLine();

                Console.Write("Enter student lastname: ");
                request.LastName = Console.ReadLine();

                Console.Write("Enter student middlename: ");
                request.MiddleName = Console.ReadLine();

                int newGender = Helper.SelectEnum("Enter student gender: \nEnter 1 for Male\nEnter 2 for Female: ", 1, 2);
                request.Gender = (Gender)newGender;

                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.Gender = request.Gender;
                student.MiddleName = request.MiddleName;
                student.UpdatedAt = DateTime.UtcNow;

                studentRepository.RefreshFile();

                Console.WriteLine($"Student record with code \"{student.AdmissionNo}\" is successfully updated!");
                return;
            }

            Console.WriteLine($"Student not found!");
        }
    }
}
