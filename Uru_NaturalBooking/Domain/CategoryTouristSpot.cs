using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Domain
{
    public class CategoryTouristSpot
    {
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Guid TouristSpotId { get; set; }

        public virtual TouristSpot TouristSpot { get; set; }

        public CategoryTouristSpot() { }

		public override bool Equals(object obj)
		{

			CategoryTouristSpot categoryTouristSpot = obj as CategoryTouristSpot;
			if(categoryTouristSpot == null || Convert.IsDBNull(categoryTouristSpot))
			{
				return false;
			}			
			return CategoryId.Equals(categoryTouristSpot.CategoryId);
		}

	}
}
