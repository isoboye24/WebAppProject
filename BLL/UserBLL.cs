using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userDAO = new UserDAO();

        public void AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.NameSurname = model.Name;
            user.ImagePath = model.ImagePath;
            user.isAdmin = model.isAdmin;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            // In order to see all users, I set isDeleted = false 
            user.isDeleted = false;
            int ID = userDAO.AddUser(user);
            LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID);
        }

        public List<UserDTO> GetUsers()
        {            
            return userDAO.GetUsers();
        }

        public UserDTO GetUserWithID(int ID)
        {
            return userDAO.GetUserWithID(ID);
        }

        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userDAO.GetUserWithUsernameAndPassword(model);
            return dto;
        }

        public string UpdateUser(UserDTO model)
        {
            string oldImagePath = userDAO.UpdateUser(model);
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID);
            return oldImagePath;
        }
    }
}
