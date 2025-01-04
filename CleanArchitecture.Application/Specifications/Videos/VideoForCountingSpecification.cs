using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Videos
{
    public class VideoForCountingSpecification : BaseSpecification<Video>
    {
        public VideoForCountingSpecification(VideoSpecificationParams videoParams)
            : base(
                  x =>
                  (string.IsNullOrEmpty(videoParams.Search) || x.Nombre!.Contains(videoParams.Search)) && 
                  (!videoParams.DirectorId.HasValue || x.DirectorId == videoParams.DirectorId) &&
                  (!videoParams.StreamerId.HasValue || x.StreamerId == videoParams.StreamerId)
                  )
        {
        }
    }
}
