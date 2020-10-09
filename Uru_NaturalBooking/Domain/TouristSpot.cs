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
                throw new TouristSpotException(MessageException.ErrorIsEmpty);
            }

            if (IsLongerThanTheLimit())
            {
                throw new TouristSpotException(MessageException.ErrorIsLongerThanTheLimit);
            }
            if (NotHasCategoriesDefined())
            {
                throw new TouristSpotException(MessageException.ErrorDoesNotHaveCategory);
            }

            if (IsInvalidPicture())
            {
                throw new TouristSpotException(MessageException.ErrorPicture); 
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
