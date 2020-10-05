using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RegionModel : ModelBase<Region, RegionModel>
    {

        public Guid Id { get; set; }

        public enum RegionName { Región_Metropolitana, Región_Centro_Sur, Región_Este, Región_Literal_Norte, Región_Corredor_Pajaros_Pintados }

        public RegionName Name { get; set; }

        public override Region ToEntity() => new Region()
        {
            Id = Id,
            Name = (Region.RegionName)Name
        };

        protected override RegionModel SetModel(Region region)
        {
            Id = region.Id;
            Name = (RegionModel.RegionName)region.Name;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is RegionModel model &&
                   Id.Equals(model.Id) &&
                   Name == model.Name;
        }
    }
}
