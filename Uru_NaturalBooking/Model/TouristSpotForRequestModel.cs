using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TouristSpotForRequestModel : ModelBase<TouristSpot, TouristSpotForRequestModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid RegionId{ get; set; }

        public List<Guid> ListOfCategoriesId { get; set; }

       

        public override TouristSpot ToEntity() => new TouristSpot()
        {
            Id = Id,
            Name = Name,
            Description = Description
        };

        protected override TouristSpotForRequestModel SetModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            Description = touristSpot.Description;
            RegionId = touristSpot.Region.Id;
            ListOfCategoriesId = touristSpot.ListOfCategories.ConvertAll(c => c.CategoryId);
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is TouristSpotForRequestModel model &&
                   Id.Equals(model.Id) &&
                   Name == model.Name;
        }
    }
}
