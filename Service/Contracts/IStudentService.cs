using StudentManagementApp.Entity.Dtos;
using StudentManagementApp.Entity;

namespace StudentManagementApp.Service.Contracts
{
    public interface IStudentService
    {
        void Create(StudentDto request);
        void GetAll();
        void GetAStudent(int id);
        void Update(int id, StudentDto updateStudentDto);
        void Delete(int id);
        void PrintListView(Student student);
        void PrintDetailView(Student student);
    }
}
