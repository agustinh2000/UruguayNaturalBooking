using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForRequest
{
    public class RegionForResponseModel : ModelBaseForResponse<Region, RegionForResponseModel>
    {

        public Guid Id { get; set; }

        public enum RegionName { Región_Metropolitana, Región_Centro_Sur, Región_Este, Región_Literal_Norte, Región_Corredor_Pajaros_Pintados }

        public RegionName Name { get; set; }

        protected override RegionForResponseModel SetModel(Region region)
        {
            Id = region.Id;
            Name = (RegionForResponseModel.RegionName)region.Name;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is RegionForResponseModel model &&
                   Id.Equals(model.Id) &&
                   Name == model.Name;
        }
    }
}
