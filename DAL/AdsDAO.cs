using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO : PostContext
    {
        public int AddAds(AdsTable ads)
        {
			try
			{
				db.AdsTables.Add(ads);
				db.SaveChanges();
				return ads.AdsID;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }
}
