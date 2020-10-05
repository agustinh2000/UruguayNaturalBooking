using Domain;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public class TouristSpotForResponseModel : ModelBase<TouristSpot, TouristSpotForResponseModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public RegionModel RegionModel { get; set; }

        public List<CategoryModel> ListOfCategoriesModel { get; set; }

       
        public override TouristSpot ToEntity() => new TouristSpot()
        {
            Id = Id,
            Name = Name,
            Description = Description
        };

        protected override TouristSpotForResponseModel SetModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            Description = touristSpot.Description;
            RegionModel = RegionModel.ToModel(touristSpot.Region);
            ListOfCategoriesModel = touristSpot.ListOfCategories.ConvertAll(m => CategoryModel.ToModel(m.Category));
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is TouristSpotForResponseModel model &&
                   Id.Equals(model.Id) &&
                   Name == model.Name;
        }



    }
}
