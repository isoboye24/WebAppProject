using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MetaDAO : PostContext
    {
        public int AddMeta(Meta meta)
        {
			try
			{
				db.Metas.Add(meta);
				db.SaveChanges();
				return meta.MetaID;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

        public List<MetaDTO> GetMetaData()
        {

            List<MetaDTO> metaList = new List<MetaDTO>();
            List<Meta> list = db.Metas.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            foreach (var item in list)
            {
                MetaDTO dto = new MetaDTO();
                dto.MetaID = item.MetaID;
                dto.Name = item.Name;
                dto.MetaContent = item.MetaContent;
                metaList.Add(dto);
            }
            return metaList;
        }

        public MetaDTO GetMetaWithID(int ID)
        {
            Meta meta = db.Metas.First(x => x.MetaID == ID);
            MetaDTO dto = new MetaDTO();
            dto.MetaID = meta.MetaID;
            dto.Name = meta.Name;
            dto.MetaContent = meta.MetaContent;
            return dto;
        }

        public void UpdateMeta(MetaDTO model)
        {
            try
            {
                Meta meta = db.Metas.First(x=>x.MetaID==model.MetaID);
                meta.Name = model.Name;
                meta.MetaContent = model.MetaContent;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
