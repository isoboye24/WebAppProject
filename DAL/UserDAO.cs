using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class UserDAO:PostContext
    {
        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.UserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDTO> GetUsers()
        {
            try
            {
                List<UserDTO> userList = new List<UserDTO>();
                List<T_User> list = db.T_User.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    UserDTO dto = new UserDTO();
                    dto.ID = item.UserID;
                    dto.Name = item.NameSurname;
                    dto.Username = item.Username;
                    dto.ImagePath = item.ImagePath;
                    dto.Password = item.Password;
                    dto.Email = item.Email;
                    dto.isAdmin = item.isAdmin;
                    userList.Add(dto);
                }
                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserDTO GetUserWithID(int ID)
        {
            try
            {
                T_User user = db.T_User.First(x => x.UserID == ID);
                UserDTO dto = new UserDTO();
                dto.ID = user.UserID;
                dto.Name = user.NameSurname;
                dto.Username = user.Username;
                dto.Password = user.Password;
                dto.isAdmin = user.isAdmin;
                dto.Email = user.Email;
                dto.ImagePath = user.ImagePath;
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            T_User user = db.T_User.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user != null && user.UserID != 0)
            {
                dto.ID = user.UserID;
                dto.Username = user.Username;
                dto.Name = user.NameSurname;
                dto.ImagePath = user.ImagePath;
                dto.isAdmin = user.isAdmin;
            }
            return dto;
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                T_User user = db.T_User.First(x => x.UserID == model.ID);
                string oldImagePath = user.ImagePath;
                user.NameSurname = model.Name;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Email = model.Email;
                user.isAdmin = model.isAdmin;
                if (model.ImagePath != null)
                {
                    user.ImagePath = model.ImagePath;
                }
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
