using Dapper;
using DapperApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApi.Repository
{
    public class StudentRepository
    {
        private string connectionString = "";

        public StudentRepository()
        {
            connectionString = @"Data Source=DESKTOP-333MNK1\SQLSERVER2020;Initial Catalog=BlazorDapper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public IDbConnection connection
        {
            get { return new SqlConnection(connectionString); }
        }
        public async Task<IEnumerable<StudentInfo>> GetStudents()
        {
            IEnumerable<StudentInfo> student;

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    student = await dbConnection.QueryAsync<StudentInfo>("spStudent_GetAll", commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                        dbConnection.Close();
                }
                return student;
            }
        }
        public async Task<bool> CreateStudent(StudentInfo student)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("StudentName", student.StudentName);
            parameters.Add("TotalMarks", student.TotalMarks);

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {

                    {
                        await dbConnection.ExecuteAsync("spStudent_Insert", parameters, commandType: CommandType.StoredProcedure);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                        dbConnection.Close();
                }
                return true;
            }
        }
        public async Task<StudentInfo> GetStudentById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            StudentInfo student = new StudentInfo();
            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    student = await dbConnection.QueryFirstOrDefaultAsync<StudentInfo>("spStudent_GetOne", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                        dbConnection.Close();
                }
                return student;
            }
        }
        public async Task<bool> UpdateStudent(StudentInfo updatedStudent)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedStudent.StudID, DbType.Int32);
            parameters.Add("StudentName", updatedStudent.StudentName);
            parameters.Add("TotalMarks", updatedStudent.TotalMarks);

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    await dbConnection.ExecuteAsync("spStudent_Update", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                        dbConnection.Close();
                }
                return true;
            }
        }
        public async Task<bool> DeleteStudent(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    await dbConnection.ExecuteAsync("spStudent_Delete", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                        dbConnection.Close();
                }
                return true;
            }
        }
    }
}
