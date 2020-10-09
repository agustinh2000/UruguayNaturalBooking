using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using static Domain.Region;

namespace Model.ForResponse
{
    public class RegionForResponseModel : ModelBaseForResponse<Region, RegionForResponseModel>
    {
        public Guid Id { get; set; }

        public RegionName Name { get; set; }

        public string DescriptionOfName { get; set; }

        protected override RegionForResponseModel SetModel(Region region)
        {
            Id = region.Id;
            Name = region.Name;
            DescriptionOfName = region.GetEnumDescription(); 
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is RegionForResponseModel model &&
                   Id.Equals(model.Id) &&
                   Name == model.Name
                   && DescriptionOfName.Equals(model.DescriptionOfName);
        }

    }
}
