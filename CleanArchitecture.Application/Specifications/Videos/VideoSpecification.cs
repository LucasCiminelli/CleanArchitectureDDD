using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Videos
{
    public class VideoSpecification : BaseSpecification<Video>
    {

        public VideoSpecification(VideoSpecificationParams videoParams)
            : base(
                  x =>
                  (string.IsNullOrEmpty(videoParams.Search) || x.Nombre!.Contains(videoParams.Search)) &&
                  (!videoParams.DirectorId.HasValue || x.DirectorId == videoParams.DirectorId) &&
                  (!videoParams.StreamerId.HasValue || x.StreamerId == videoParams.StreamerId)
                  )
        {

            AddInclude(p => p.Director!);
            AddInclude(p => p.Streamer!);
            AddInclude(p => p.Actores!);

            ApplyPaging(videoParams.PageSize * (videoParams.PageIndex - 1), videoParams.PageSize);

            if (!string.IsNullOrEmpty(videoParams.Sort))
            {
                switch (videoParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Nombre!);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(p => p.Nombre!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }

        }

    }
}
