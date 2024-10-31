﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetToursByUserId(long userId);

        List<Equipment> GetEquipment(long tourId);  
        void AddEquipmentToTour(long tourId, long equipmentId);
        void RemoveEquipmentFromTour(long tourId, long equipmentId);

        Tour GetSpecificTourByUser(long id, long userId);
        void Archive(long id, long authorId);
        void Reactivate(long id, long authorId);

    }
}
