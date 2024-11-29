using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterDto> CreateEncounter(EncounterDto encounterDto);
        Result<PagedResult<EncounterDto>> Get(int page, int pageSize);
        Result<PagedResult<EncounterDto>> GetInRadius(double radius, double lat, double lon);
        Result<EncounterDto> ActivateEncounter(long userId, long encounterId, double longitude, double latitude);
        Result<EncounterDto> CompleteEncounter(long userId, long encounterId);
        Result<EncounterDto> GetByLatLong(double latitude, double longitude);
        void ApproveEncounter(long id);
        void RejectEncounter(long id);
        Result<PagedResult<EncounterDto>> GetPendingRequest();
        Result<PagedResult<EncounterDto>> GetActiveForUser(long userId);

    }
}
