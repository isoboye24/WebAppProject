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
    }
}
