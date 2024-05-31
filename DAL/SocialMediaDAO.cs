using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        public int AddSocialMedia(SocialMedia socialMedia)
        {
			try
			{
				db.SocialMedias.Add(socialMedia);
				db.SaveChanges();
				return socialMedia.SocialMediaID;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
			try
			{
                List<SocialMediaDTO> socialMediaList = new List<SocialMediaDTO>();
				List<SocialMedia> list = db.SocialMedias.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
				foreach (var item in list)
				{
					SocialMediaDTO dto = new SocialMediaDTO();
					dto.SocialMediaID = item.SocialMediaID;
					dto.Name = item.Name;
					dto.ImagePath = item.ImagePath;
					dto.Link = item.Link;
					socialMediaList.Add(dto);
                }
				return socialMediaList;
            }
			catch (Exception ex)
			{
				throw ex;
			}
            
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
			try
			{
				SocialMedia socialMedia = db.SocialMedias.First(x => x.SocialMediaID == ID);
				SocialMediaDTO dto = new SocialMediaDTO();
				dto.SocialMediaID = socialMedia.SocialMediaID;
				dto.Name = socialMedia.Name;
				dto.ImagePath = socialMedia.ImagePath;
				dto.Link = socialMedia.Link;
				return dto;
            }
			catch (Exception ex)
			{
				throw ex;
			}            
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
			try
			{
				SocialMedia social = db.SocialMedias.First(x => x.SocialMediaID == model.SocialMediaID);
				string oldImagePath = social.ImagePath;
				social.Name = model.Name;
				social.Link = model.Link;
				if (model.ImagePath != null)
				{
					social.ImagePath = model.ImagePath;
				}
				social.LastUpdateDate = DateTime.Now;
				social.LastUpdateUserID = UserStatic.UserID;
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
