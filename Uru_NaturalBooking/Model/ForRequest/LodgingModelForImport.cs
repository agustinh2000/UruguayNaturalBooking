using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Model.ForRequest
{
    public class LodgingModelForImport : ModelBaseForRequest<Lodging, LodgingModelForImport>
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public int QuantityOfStars { get; set; }

        public string Address { get; set; }

        [XmlArray("Images")]
        [XmlArrayItem("ImagePath")]
        public List<string> Images { get; set; }

        public double PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true;

        public TouristSpotModelForImport TouristSpot { get; set; }

        public override Lodging ToEntity() => new Lodging()
        {
            Name = Name,
            Description = Description,
            QuantityOfStars = QuantityOfStars,
            Address = Address,
            PricePerNight = PricePerNight,
            IsAvailable = IsAvailable
        };

        public override bool Equals(object obj)
        {
            return obj is LodgingModelForImport import &&
                   Name.Equals(import.Name) &&
                   Description.Equals(import.Description) &&
                   QuantityOfStars == import.QuantityOfStars &&
                   Address.Equals(import.Address) &&
                   Images.SequenceEqual(import.Images) &&
                   PricePerNight == import.PricePerNight &&
                   TouristSpot.Equals(import.TouristSpot);
        }
    }
}
