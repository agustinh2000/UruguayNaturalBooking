using System;

namespace Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public enum RegionName { Región_Metropolitana, Región_Centro_Sur, Región_Este, Región_Literal_Norte, Región_Corredor_Pajaros_Pintados }

        public RegionName Name { get; set; }
		
		
	}
}
