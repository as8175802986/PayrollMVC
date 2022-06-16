using System;
using CommonLayer.UserModel;
using RepositoryLayer.InterFace;
using BusinessLayer.Interface;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL UserRL;
        public UserBL(IUserRL userRL)
        {
            this.UserRL = userRL;
        }
        public string AddEmployee(UserModel userModel)
        {
            try
            {
                return UserRL.AddEmployee(userModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<UserModel> EmployeeList()
        {
            try
            {
                return this.UserRL.EmployeeList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserModel getEmployeeById(int? id)
        {
            try
            {
                return this.UserRL.getEmployeeById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string UpdateEmployeeDetails(UserModel update)
        {
            try
            {
                return UserRL.UpdateEmployeeDetails(update);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<UserModel> GetAlldatabyEmployeeID(int EmployeeID)
        {
            try
            {
                return UserRL.GetAlldatabyEmployeeID(EmployeeID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void deleteEmployee(UserModel employee)
        {
            try
            {
                this.UserRL.deleteEmployee(employee);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


    }
}
