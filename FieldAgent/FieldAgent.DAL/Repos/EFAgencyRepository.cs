﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repos
{
    class EFAgencyRepository : IAgencyRepository
    {
        public Response Delete(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<Agency> Get(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Agency>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Response<Agency> Insert(Agency agency)
        {
            throw new NotImplementedException();
        }

        public Response Update(Agency agency)
        {
            throw new NotImplementedException();
        }
    }
}
