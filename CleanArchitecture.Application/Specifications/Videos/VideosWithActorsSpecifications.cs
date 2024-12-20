using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Videos
{
    public class VideosWithActorsSpecifications : BaseSpecification<Video>
    {
        public VideosWithActorsSpecifications()
        {

            AddInclude(v => v.Actores!);

        }

        public VideosWithActorsSpecifications(int streamerId) : base(v => v.StreamerId == streamerId)
        {

            AddInclude(v => v.Actores!);

        }
    }
}
