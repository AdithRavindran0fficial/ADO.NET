using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Task_1.Models;

namespace Task_1.Services
{
    public class StudentService : IService
    {
        public List <Student> Students  = new List<Student> ();
        private string ConnectingString;
        private IConfiguration _configuration;
        public StudentService(IConfiguration configuration)
        {
           this._configuration = configuration;
            this.ConnectingString = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public List<Student> GetStudents()
        {
            using (SqlConnection con = new SqlConnection(ConnectingString))
            {
                SqlCommand cmd = new SqlCommand("select * from student", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Student student = new Student();
                    student.Id = (int)sdr["studentid"];
                    student.F_name = (string)sdr["Firstname"];
                    student.L_name = (string)sdr["Lastname"];
                    student.Age = (int)sdr["Age"];
                    Students.Add(student);

                }      
            }
            return Students;
        }
        public Student Get_id(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectingString)) 
            {
                SqlCommand cmd = new SqlCommand("select * from student where studentid=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);
                Student student = new Student();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    student.F_name = (string)sdr["Firstname"];
                    student.L_name = (string)sdr["Lastname"];
                    student.Age = (int)sdr["Age"];
                    return student;
                    
                }
                
                else
                {
                    return null;
                    
                }

                
                
            }
            
        }
        public int Post(Student student) 
        {

        using(SqlConnection con = new SqlConnection(ConnectingString))
            {
                SqlCommand cmd = new SqlCommand("insert into student(Firstname,Lastname,Age) values(@firstname,@lastname,@Age)",con);
                con.Open();
                cmd.Parameters.AddWithValue("@firstname", student.F_name);
                cmd.Parameters.AddWithValue("@lastname", student.L_name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                int row = cmd.ExecuteNonQuery();
                con.Close();
                return row;

            }      
        }
        public Student update(int id, Student student)
        {
            using (SqlConnection conn = new SqlConnection(ConnectingString))
            {
                SqlCommand c = new SqlCommand("select count(*) from student where studentid=@id_", conn);
                conn.Open();
                c.Parameters.AddWithValue("@id_", id);
                var exist = (int)c.ExecuteScalar();

                if (exist == 0)
                {
                    return null;
                }

            SqlCommand cmd = new SqlCommand($"update student set Firstname = @fname,Lastname=@lname,Age=@age where studentid=@id", conn);

            cmd.Parameters.AddWithValue("@fname", student.F_name);
            cmd.Parameters.AddWithValue("@lname", student.L_name);
            cmd.Parameters.AddWithValue("@age", student.Age);
                cmd.Parameters.AddWithValue("@id", id);
            var row = cmd.ExecuteNonQuery();
            return student;
        }
            
        }
        public int Delete(int id)
        {
            using(SqlConnection c = new SqlConnection(ConnectingString))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from student where studentid=@id", c);
                cmd.Parameters.AddWithValue("@id", id);
                int row = (int)cmd.ExecuteScalar();
                if (row == 0)
                {
                    return 0;
                }          
                SqlCommand comm = new SqlCommand("delete from student where studentid= @id",c);
                
                comm.Parameters.AddWithValue("@id", id);
                int row_ = (int)comm.ExecuteNonQuery();
                return row_;
            }
        }


    }
}
