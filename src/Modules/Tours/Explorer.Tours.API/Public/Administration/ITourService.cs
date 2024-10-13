using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourService
    {
        Result <TourDto> Create (TourDto dto);

        Result <List<TourDto>> GetByUserId (long userId);

        Result<TourDto> Get(int id);

        Result<TourDto> AddKeyPoint(long tourId, long keyPointId);
    }
}
