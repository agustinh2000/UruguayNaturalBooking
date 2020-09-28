using System;
using System.Collections.Generic;
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
			if (obj == null)
			{
				return false;
			}
			else if (this.GetType() != obj.GetType())
			{
				return false;
			}
			else
			{
				CategoryTouristSpot categoryTouristSpot = (CategoryTouristSpot)obj;
				return CategoryId.ToString().Equals(categoryTouristSpot.CategoryId.ToString());
			}
		}



	}
}
