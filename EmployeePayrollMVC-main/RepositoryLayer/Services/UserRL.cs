using CommonLayer.UserModel;
using System;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace RepositoryLayer.Services
{
    public class UserRL : InterFace.IUserRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; set; }
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public string AddEmployee(UserModel userModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayroll"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "Sp_AddEmployee";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@EmployeeName", userModel.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@ProfileImage", userModel.ProfileImage);
                    sqlCommand.Parameters.AddWithValue("@Gender", userModel.Gender);
                    sqlCommand.Parameters.AddWithValue("@Department", userModel.Department);
                    sqlCommand.Parameters.AddWithValue("@Salary", userModel.Salary);
                    sqlCommand.Parameters.AddWithValue("@StartDate", userModel.StartDate);
                    sqlCommand.Parameters.AddWithValue("@Notes", userModel.Notes);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Employee Added succssfully";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public List<UserModel> EmployeeList()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayroll"));
            try
            {
                List<UserModel> listemployee = new List<UserModel>();
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SelectAllEmployee", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UserModel employee = new UserModel();
                        employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                        employee.EmployeeName = rdr["EmployeeName"].ToString(); 
                        employee.ProfileImage = rdr["ProfileImage"].ToString();
                        employee.Gender = rdr["Gender"].ToString();
                        employee.Department = rdr["Department"].ToString();
                        employee.Salary = rdr["Salary"].ToString();
                        employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                        employee.Notes = rdr["Notes"].ToString();
                        //employee.RegisteredDate = Convert.ToDateTime(rdr["RegisteredDate"]);




                        listemployee.Add(employee);
                    }
                    sqlConnection.Close();

                }
                return listemployee;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public List<UserModel> GetAlldatabyEmployeeID(int EmployeeID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayroll"));
            try
            {
                using (sqlConnection)
                {
                    List<UserModel> EmpList = new List<UserModel>();
                    string storeprocedure = "spGetEmployeeid";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    UserModel employee = new UserModel();
                    sqlCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    sqlConnection.Open();
                    SqlDataReader rdr = sqlCommand.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                            employee.EmployeeName = rdr["EmployeeName"].ToString();
                            employee.ProfileImage = rdr["ProfileImage"].ToString();
                            employee.Gender = rdr["Gender"].ToString();
                            employee.Department = rdr["Department"].ToString();
                            employee.Salary = rdr["Salary"].ToString();
                            employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                            employee.Notes = rdr["Notes"].ToString();
                            EmpList.Add(employee);
                        }
                        return EmpList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public UserModel getEmployeeById(int? id)
        {
            UserModel employee = new UserModel();

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))
            {
                string sqlQuery = "SELECT * FROM RegisterEmployee WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.EmployeeName = rdr["EmployeeName"].ToString();
                    employee.ProfileImage = rdr["ProfileImage"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = rdr["Salary"].ToString();
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                    
                }
            }
            return employee;
        }
        public string UpdateEmployeeDetails(UserModel update)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_UpdateEmployee";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@EmployeeID", update.EmployeeID);
                    sqlCommand.Parameters.AddWithValue("@EmployeeName", update.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@ProfileImage", update.ProfileImage);
                    sqlCommand.Parameters.AddWithValue("@Gender", update.Gender);
                    sqlCommand.Parameters.AddWithValue("@Department", update.Department);
                    sqlCommand.Parameters.AddWithValue("@Salary", update.Salary);
                    sqlCommand.Parameters.AddWithValue("@StartDate", update.StartDate);
                    sqlCommand.Parameters.AddWithValue("@Notes", update.Notes);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Update book details Unsuccessful";
                    }
                    else
                    {
                        return "Details Updated Successfully";
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void deleteEmployee(UserModel employeemodel)
        {
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", employeemodel.EmployeeID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
