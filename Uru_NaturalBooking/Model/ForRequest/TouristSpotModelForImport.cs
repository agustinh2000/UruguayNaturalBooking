using Domain;
using Model.ForResponseAndRequest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Model.ForRequest
{
    public class TouristSpotModelForImport : ModelBaseForRequest<TouristSpot, TouristSpotModelForImport>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid RegionId { get; set; }

        public string ImagePath { get; set; }

        [XmlArray("ListOfCategoriesId")]
        [XmlArrayItem("CategoryId")]
        public List<Guid> ListOfCategoriesId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TouristSpotModelForImport import &&
                   Name.Equals(import.Name) &&
                   Description.Equals(import.Description) &&
                   ImagePath.Equals(import.ImagePath);
        }

        public override TouristSpot ToEntity() => new TouristSpot()
        {
            Id= Id, 
            Name = Name,
            Region= new Region() { Id= RegionId}, 
            Description = Description,
            Image = new Picture()
            {
                Id = Guid.NewGuid(),
                Path = ImagePath
            }, 
            ListOfCategories = ListOfCategoriesId.ConvertAll( c => new CategoryTouristSpot() { CategoryId = c})
        };
    }
}
