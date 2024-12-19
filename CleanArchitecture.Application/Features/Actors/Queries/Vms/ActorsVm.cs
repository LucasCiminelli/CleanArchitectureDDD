using CleanArchitecture.Application.Features.Videos.Queries.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.Vms
{
    public class ActorsVm
    {

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public ICollection<VideosVm>? Videos { get; set; }
    }
}
                                                                                                            