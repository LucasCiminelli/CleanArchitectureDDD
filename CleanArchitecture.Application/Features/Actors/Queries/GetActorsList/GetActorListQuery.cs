﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.GetActorsList
{
    internal class GetActorListQuery
    {


        public string _Username { get; set; }

        public GetActorListQuery(string username)
        {
            _Username = username;
        }

    }
}
