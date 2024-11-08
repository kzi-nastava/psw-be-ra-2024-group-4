﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.ProfileMessages
{
    public interface IProfileMessageRepository
    {
        public ProfileMessage Create(ProfileMessage profileMessage);
        PagedResult<ProfileMessage> GetByUserId(long userId);
        PagedResult<ProfileMessage> GetByClubId(long clubId);
        public ProfileMessage Update(ProfileMessage aggregateRoot);
        public bool Delete(long aggregateRootId);
    }
}
