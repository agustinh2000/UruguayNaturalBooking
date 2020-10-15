using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForRequest
{
    public class TouristSpotForRequestModel : ModelBaseForRequest<TouristSpot, TouristSpotForRequestModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid RegionId{ get; set; }

        public string ImagePath { get; set; }

        public List<Guid> ListOfCategoriesId { get; set; }

        public override TouristSpot ToEntity() => new TouristSpot()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Image = new Picture()
            {
                Id= Guid.NewGuid(),
                Path=ImagePath
            }
        };
    }
}
