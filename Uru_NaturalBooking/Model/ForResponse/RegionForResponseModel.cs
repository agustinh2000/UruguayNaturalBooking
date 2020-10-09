using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Model.ForResponse
{
    public class RegionForResponseModel : ModelBaseForResponse<Region, RegionForResponseModel>
    {

        public Guid Id { get; set; }

        public enum RegionName { 
            [Description("Region Metropolitana")]
            Región_Metropolitana,
            [Description("Region Centro Sur")]
            Región_Centro_Sur,
            [Description("Region Este")]
            Región_Este,
            [Description("Region Literal Norte")] 
            Región_Literal_Norte,
            [Description("Region Corredor Pajaros Pintados")]
            Región_Corredor_Pajaros_Pintados }

        public RegionName Name { get; set; }

        public string DescriptionOfName { get; set; }

        protected override RegionForResponseModel SetModel(Region region)
        {
            Id = region.Id;
            Name = (RegionForResponseModel.RegionName)region.Name;
            DescriptionOfName = GetEnumDescription(Name); 
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is RegionForResponseModel model &&
                   Id.Equals(model.Id) &&
                   Name == model.Name
                   && DescriptionOfName.Equals(model.DescriptionOfName);
        }

        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }

    }
}
