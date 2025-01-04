using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Videos
{
    public class VideoSpecificationParams : SpecificationParams
    {
        public int? StreamerId { get; set; }
        public int? DirectorId { get; set; }

    }
}
