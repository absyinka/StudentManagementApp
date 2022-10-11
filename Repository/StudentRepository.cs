using Microsoft.VisualBasic;
using StudentManagementApp.Commons;
using StudentManagementApp.Entity;
using StudentManagementApp.Entity.Dtos;
using StudentManagementApp.Enums;
using StudentManagementApp.Repository.Contracts;

namespace StudentManagementApp.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public static List<Student> students;
        public StudentRepository()
        {
            students = new List<Student>();
            ReadFromFile();
        }

        private void ReadFromFile()
        {
            try
            {
                if (File.Exists(FileAndFilePath.fullPath))
                {
                    var lines = File.ReadAllLines(FileAndFilePath.fullPath);
                    foreach (var line in lines)
                    {
                        var student = Student.ToStudent(line);
                        students.Add(student);
                    }
                }
                else
                {
                    Directory.CreateDirectory(FileAndFilePath.directory);
                    var fullPath = Path.Combine(FileAndFilePath.directory, FileAndFilePath.fileName);
                    using (File.Create(fullPath))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }

        public Student GetByAdmissionNo(string admissionNo)
        {
            return students.Find(s => s.AdmissionNo == admissionNo);

        }

        public Student GetById(int id)
        {
            return students.Find(s => s.Id == id);
        }

        public Student GetByIdOrAdmissionNo(int id, string admissionNo)
        {
            return students.Find(s => s.Id == id || s.AdmissionNo == admissionNo);
        }

        public void RefreshFile()
        {
            try
            {
                using StreamWriter writer = new(FileAndFilePath.fullPath);

                foreach (var student in students)
                {
                    writer.WriteLine(student.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void WriteToFile(Student entity)
        {
            try
            {
                using StreamWriter writer = new(FileAndFilePath.fullPath, true);
                writer.WriteLine(entity.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
