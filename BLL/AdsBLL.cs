using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Configuration;

namespace BLL
{
    public class AdsBLL
    {
        AdsDAO dao = new AdsDAO();
        public void AddAds(AdsDTO model)
        {            
            AdsTable ads = new AdsTable();
            ads.Name = model.Name;
            ads.ImagePath = model.ImagePath;
            ads.Link = model.Link;
            ads.Size = model.ImageSize;
            ads.AddDate = DateTime.Now;
            ads.LastUpdateDate = DateTime.Now;
            ads.LastUpdateUserID =UserStatic.UserID;
            int ID = dao.AddAds(ads);
            LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Ads, ID);
        }
    }
}
