using DomainException;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual List<CategoryTouristSpot> ListOfTouristSpot { get; set; }

        public Category() {
			ListOfTouristSpot = new List<CategoryTouristSpot>(); 
		}

        public void VerifyFormat()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new CategoryException(MessageException.ErrorIsEmpty); 
            }
        }

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
                Category category = (Category)obj;
                return Name.Equals(category.Name);
            }
        }
    }
}
