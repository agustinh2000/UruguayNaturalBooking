using DomainException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class TouristSpot
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Picture Image { get; set; }

        public virtual Region Region { get; set; }

        public virtual List<CategoryTouristSpot> ListOfCategories { get; set; }

        public TouristSpot() {
            ListOfCategories = new List<CategoryTouristSpot>(); 
        }

        public void VerifyFormat()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Description))
            {
                throw new TouristSpotException(MessageExceptionDomain.ErrorIsEmpty);
            }

            if (IsLongerThanTheLimit())
            {
                throw new TouristSpotException(MessageExceptionDomain.ErrorIsLongerThanTheLimit);
            }
            if (NotHasCategoriesDefined())
            {
                throw new TouristSpotException(MessageExceptionDomain.ErrorDoesNotHaveCategory);
            }

            if (IsInvalidPicture())
            {
                throw new TouristSpotException(MessageExceptionDomain.ErrorPicture); 
            }
        }

        private bool IsLongerThanTheLimit()
        {
            return Description.Length > 2000;
        }

        private bool NotHasCategoriesDefined()
        {
            return ListOfCategories.Count == 0;
        }

        private bool IsInvalidPicture()
        {
            return Image==null || String.IsNullOrEmpty(Image.Path); 
        }

        public bool HasCategoriesSearched(List<Guid> listOfCategoriesIdSearched)
        {
            var list = ListOfCategories;
            return !listOfCategoriesIdSearched.Except(ListOfCategories.ConvertAll(cat => cat.CategoryId)).Any(); 
        }

        public void UpdateAttributes(TouristSpot touristSpotWithModification)
        {
            Name = touristSpotWithModification.Name;
            Description = touristSpotWithModification.Description;
            Image = touristSpotWithModification.Image;
            Region = touristSpotWithModification.Region;
            ListOfCategories = touristSpotWithModification.ListOfCategories;
        }
    }
}
