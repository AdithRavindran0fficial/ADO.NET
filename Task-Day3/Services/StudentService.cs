using Microsoft.Data.SqlClient;
using System.Data;
using Task_Day3.Models;

namespace Task_Day3.Services
{
    public class StudentService:IStudentService
    {
        private List<Student> students = new List<Student>();
        private IConfiguration _configuration;
        private string connection;

        public StudentService(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public List<Student> Get_All()
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAll",con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                foreach(DataRow row in dataset.Tables[0].Rows)
                {
                    Student stud = new Student();
                    stud.Id = (int)row["studentid"];
                    stud.Firstname = row["Firstname"].ToString();
                    stud.Lastname = row["Lastname"].ToString();
                    stud.Age = (int)row["Age"];
                    students.Add(stud);
                }

            }
            return students;
        }
       public  Student Post(Student student)
        {
            using(SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_post", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", student.Firstname);
                cmd.Parameters.AddWithValue("@lname", student.Lastname);
                cmd.Parameters.AddWithValue("@age", student.Age);
                cmd.ExecuteNonQuery();
                return student;
            }
        }

        public Student Get_by_id(int id)
        {
            var exist = students.FirstOrDefault(s => s.Id == id);
            return exist;       
        }

        public Student Update(int id, Student student)
        {
            var exist = students.FirstOrDefault(s => s.Id == id);
            if (exist == null) 
            {
                return null;
            }
            using(SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fname", student.Firstname);
                cmd.Parameters.AddWithValue("@lname", student.Lastname);
                cmd.Parameters.AddWithValue("@age",student.Age);
                cmd.ExecuteNonQuery();
                exist.Age = student.Age;
                exist.Firstname = student.Firstname;
                exist.Lastname = student.Lastname;
                return exist;

            }
        }
       public  bool Delete(int id)
        {
            var exist = students.FirstOrDefault(s => s.Id == id);
            if (exist == null)
            {
                return false;
            }
            using(SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

    }
    }

