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
                throw new CategoryException(MessageExceptionDomain.ErrorIsEmpty); 
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Name.Equals(category.Name);
        }
    }
}
