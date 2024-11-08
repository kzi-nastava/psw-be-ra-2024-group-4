using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ResourceDto
    {
        public enum ResourceType { TOUR, BLOG }
        public ResourceType Type { get; }
        public long EntityId { get; }
    }
}
