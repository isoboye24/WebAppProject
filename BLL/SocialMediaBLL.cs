using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO dao = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO model)
        {
            SocialMedia socialMedia = new SocialMedia();
            socialMedia.Name = model.Name;
            socialMedia.Link = model.Link;
            socialMedia.ImagePath = model.ImagePath;
            socialMedia.AddDate = DateTime.Now;
            socialMedia.LastUpdateUserID = UserStatic.UserID;
            socialMedia.LastUpdateDate = DateTime.Now;
            int ID = dao.AddSocialMedia(socialMedia);
            LogDAO.AddLog(General.ProcessType.SocialAdd, General.TableName.Social, ID);
            return true;
        }

        public SocialMediaDTO GetSocialMediaData()
        {
            throw new NotImplementedException();
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
            dtoList = dao.GetSocialMedias();
            return dtoList;
        }
    }
}
