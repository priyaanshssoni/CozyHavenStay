using System;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.RepositoryAbsractions
{
    public interface ICityRepository : IRepository<int,City>
    {
        Task<IEnumerable<City>> GetTopVisitedCities(int count);
       
    }
}

