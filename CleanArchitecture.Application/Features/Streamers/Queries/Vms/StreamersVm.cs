using CleanArchitecture.Application.Features.Videos.Queries.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Queries.Vms
{
    public class StreamersVm
    {

        public string? Nombre { get; set; }
        public string? Url { get; set; }
        public virtual ICollection<VideosVm>? Videos { get; set; }
    }
}
