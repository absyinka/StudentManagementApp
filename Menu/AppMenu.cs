using StudentManagementApp.Entity.Dtos;
using StudentManagementApp.Service.Contracts;
using StudentManagementApp.Service;

namespace StudentManagementApp.Menu
{
    public class AppMenu
    {
        private static IStudentService studentService;
        private static StudentDto studentDto;

        public AppMenu()
        {
            studentService = new StudentService();
            studentDto = new StudentDto();
        }

        public void StudentMenu()
        {
            bool flag = true;

            while (flag)
            {
                PrintStudentMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("");
                        studentService.Create(studentDto);
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        studentService.GetAll();
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        Console.Write("Enter the ID of student to search: ");
                        var studentId = int.Parse(Console.ReadLine());
                        studentService.GetAStudent(studentId);
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        Console.Write("Enter the ID of student to update: ");
                        var updateId = int.Parse(Console.ReadLine());
                        studentService.Update(updateId, studentDto);
                        Console.WriteLine("");
                        break;
                    case "5":
                        Console.WriteLine("");
                        Console.Write("Enter the ID of student to delete: ");
                        var delId = int.Parse(Console.ReadLine());
                        studentService.Delete(delId);
                        Console.WriteLine("");
                        break;
                    case "0":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        public void PrintStudentMenu()
        {
            Console.WriteLine("Enter 1 to add student record: ");
            Console.WriteLine("Enter 2 to view all student record: ");
            Console.WriteLine("Enter 3 to view a student record: ");
            Console.WriteLine("Enter 4 to update a student record: ");
            Console.WriteLine("Enter 5 to delete a student record: ");
            Console.WriteLine("Enter 0 to exit the application: ");
        }
    }
}
