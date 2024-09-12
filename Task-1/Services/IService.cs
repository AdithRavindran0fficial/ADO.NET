using Task_1.Models;

namespace Task_1.Services
{
    public interface IService
    {
        List<Student> GetStudents();

        int Post(Student student);

        Student Get_id(int id);

        Student update(int id, Student student);

        int Delete(int id);    
    }
}
