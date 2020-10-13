using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LodgingPicture
    {
        public Guid LodgingId { get; set; }

        public virtual Lodging Lodging { get; set; }

        public Guid PictureId { get; set; }

        public virtual Picture Picture { get; set; }

    }
}
