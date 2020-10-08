using Domain;
using Model.ForResponseAndRequest;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Model.ForResponse
{
    public class TouristSpotForResponseModel : ModelBaseForResponse<TouristSpot, TouristSpotForResponseModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public RegionForResponseModel RegionModel { get; set; }

        public Picture Image { get; set; }

        public List<CategoryModel> ListOfCategoriesModel { get; set; }

        protected override TouristSpotForResponseModel SetModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            Description = touristSpot.Description;
            RegionModel = RegionForResponseModel.ToModel(touristSpot.Region);
            Image = touristSpot.Image;
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
