using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Streamers
{
    public class StreamersWithVideosSpecification : BaseSpecification<Streamer>
    {

        public StreamersWithVideosSpecification()
        {
            AddInclude(p => p.Videos!);
        }

        public StreamersWithVideosSpecification(string url) : base (p => p.Url!.Contains(url)) 
        {
            AddInclude (p => p.Videos!);
        }

    }
}
