﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.UseCases
{
    public interface IChatbotService
    {
        Task<string> GetResponseAsync(string userMessage, long userId);
    }
}
