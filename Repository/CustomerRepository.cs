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
    public class CustomerRepository
    {
        private string connectionString = "";
        //private readonly SqlConnectionConfiguration _configuration;

        //public CustomerRepository(SqlConnectionConfiguration configuration)
        //{
        //    this._configuration = configuration;
        //}

        public CustomerRepository()
        {
            connectionString = @"Data Source=DESKTOP-333MNK1\SQLSERVER2020;Initial Catalog=BlazorDapper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public IDbConnection connection
        {
            get { return new SqlConnection(connectionString); }
        }
        public async Task<IEnumerable<CustomerInfo>> GetCustomers()
        {
            IEnumerable<CustomerInfo> customers;

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    customers = await dbConnection.QueryAsync<CustomerInfo>("spCustomer_GetAll", commandType: CommandType.StoredProcedure);
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
                return customers;
            }

        }
        public async Task<bool> CreateCustomer(CustomerInfo customer)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Name", customer.Name);
            parameters.Add("Age", customer.Age);
            parameters.Add("Company", customer.Company);
            parameters.Add("Email", customer.Email);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Address", customer.Address);

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {

                    {
                        await dbConnection.ExecuteAsync("spCustomer_Insert", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<CustomerInfo> GetCustomerById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            CustomerInfo customer = new CustomerInfo();
            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    customer = await dbConnection.QueryFirstOrDefaultAsync<CustomerInfo>("spCustomer_GetOne", parameters, commandType: CommandType.StoredProcedure);
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
                return customer;
            }
        }
        public async Task<bool> UpdateCustomer(CustomerInfo updatedCustomer)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedCustomer.CustID, DbType.Int32);
            parameters.Add("Name", updatedCustomer.Name, DbType.String);
            parameters.Add("Age", updatedCustomer.Age,DbType.Int32);
            parameters.Add("Company", updatedCustomer.Company,DbType.String);
            parameters.Add("Email", updatedCustomer.Email,DbType.String);
            parameters.Add("Phone", updatedCustomer.Phone,DbType.String);
            parameters.Add("Address", updatedCustomer.Address,DbType.String);

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    await dbConnection.ExecuteAsync("spCustomer_Update", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<bool> DeleteCustomer(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (IDbConnection dbConnection = connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                try
                {
                    await dbConnection.ExecuteAsync("spCustomer_Delete", parameters, commandType: CommandType.StoredProcedure);
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
