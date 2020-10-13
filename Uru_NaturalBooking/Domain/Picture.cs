using System;
using System.Collections.Generic;

namespace Domain
{
    public class Picture
    {
        public Guid Id { get; set; }
        public string Path { get; set; }

        public virtual List<LodgingPicture> LodgingPictures { get; set;}
    }
}
