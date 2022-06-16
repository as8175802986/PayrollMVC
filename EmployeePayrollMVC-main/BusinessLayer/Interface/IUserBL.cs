using CommonLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public string AddEmployee(UserModel userModel);
        List<UserModel> EmployeeList();
        UserModel getEmployeeById(int? id);
        public string UpdateEmployeeDetails(UserModel update);
        public List<UserModel> GetAlldatabyEmployeeID(int EmployeeID);
        void deleteEmployee(UserModel employeeModel);


    }
}
