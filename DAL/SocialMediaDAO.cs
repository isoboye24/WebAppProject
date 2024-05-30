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
    }
}
