using StudentManagementApp.Entity;

namespace StudentManagementApp.Repository.Contracts
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();
        Student GetById(int id);
        Student GetByAdmissionNo(string admissionNo);
        Student GetByIdOrAdmissionNo(int id, string admissionNo);
        void WriteToFile(Student entity);
        void RefreshFile();
    }
}
