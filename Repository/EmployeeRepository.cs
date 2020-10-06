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
    public class EmployeeRepository
    {
        private string connectionString = "";
        public EmployeeRepository()
        {
            connectionString = @"Data Source=DESKTOP-333MNK1\SQLSERVER2020;Initial Catalog=BlazorDapper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public IDbConnection connection
        {
            get { return new SqlConnection(connectionString); }
        }
        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            IEnumerable<EmployeeModel> employees;

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    employees = await dbConnection.QueryAsync<EmployeeModel>("spEmployee_GetAll", commandType: CommandType.StoredProcedure);
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
            }
            return employees;
        }
        public async Task<bool> CreateEmployee(EmployeeInfo employee)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name);
            parameters.Add("Department", employee.Department);
            parameters.Add("Designation", employee.Designation);
            parameters.Add("Company", employee.Company);

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    await dbConnection.ExecuteAsync("spEmployee_Insert", parameters, commandType: CommandType.StoredProcedure);
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
            }
            return true;
        }
        public async Task<EmployeeModel> SingleEmployee(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            EmployeeModel employee = new EmployeeModel();

            using (IDbConnection dbConnection = connection)
            {

                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    employee = await dbConnection.QueryFirstOrDefaultAsync<EmployeeModel>("spEmployee_GetOne", parameters, commandType: CommandType.StoredProcedure);
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
            }
            return employee;
        }
        public async Task<bool> EditEmployee(EmployeeModel employee)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", employee.Id, DbType.Int32);
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Department", employee.Department, DbType.String);
            parameters.Add("Designation", employee.Designation, DbType.String);
            parameters.Add("Company", employee.Company, DbType.String);

            using (IDbConnection dbConnection = connection)
            {

                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    await dbConnection.ExecuteAsync("spEmployee_Update", parameters, commandType: CommandType.StoredProcedure);
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
            }
            return true;
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (IDbConnection dbConnection = connection)
            {

                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    await dbConnection.ExecuteAsync("spEmployee_Delete", parameters, commandType: CommandType.StoredProcedure);
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
            }
            return true;
        }
    }
}
