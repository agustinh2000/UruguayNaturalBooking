using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public enum RegionName
        {
            [Description("Region Metropolitana")]
            Región_Metropolitana,
            [Description("Region Centro Sur")]
            Región_Centro_Sur,
            [Description("Region Este")]
            Región_Este,
            [Description("Region Literal Norte")]
            Región_Literal_Norte,
            [Description("Region Corredor Pajaros Pintados")]
            Región_Corredor_Pajaros_Pintados
        }

        public RegionName Name { get; set; }

        public string GetEnumDescription()
        {
            FieldInfo fi = Name.GetType().GetField(Name.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attributes.First().Description;            
        }

        public override bool Equals(object obj)
        {
            return obj is Region region &&
                   Id.Equals(region.Id) &&
                   Name.Equals(region.Name);
        }
    }
}
