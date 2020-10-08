using System;

namespace Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public enum RegionName { Región_Metropolitana, Región_Centro_Sur, Región_Este, Región_Literal_Norte, Región_Corredor_Pajaros_Pintados }

        public RegionName Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Region region &&
                   Id.Equals(region.Id) &&
                   Name.Equals(region.Name);
        }
    }
}
