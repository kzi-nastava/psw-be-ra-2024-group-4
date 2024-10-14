using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;

public interface IObjectService
{
    Result<ObjectDTO> Create(ObjectDTO objectDTO);
    Result<ObjectDTO> Update(ObjectDTO objectDTO);
    Result Delete(int id);
}

