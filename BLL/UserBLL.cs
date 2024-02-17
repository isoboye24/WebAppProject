﻿using System;
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
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userDAO.GetUserWithUsernameAndPassword(model);
            return dto;
        }
    }
}
