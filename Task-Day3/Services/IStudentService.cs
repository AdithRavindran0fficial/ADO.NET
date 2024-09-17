using Task_Day3.Models;

namespace Task_Day3.Services
{
    public interface IStudentService
    {
        List<Student> Get_All();
        Student Get_by_id(int id);
        Student Post(Student student);
        Student Update(int id, Student student);

        bool Delete(int id);
    }
}
