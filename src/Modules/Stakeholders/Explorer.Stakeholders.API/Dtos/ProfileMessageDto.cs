using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ProfileMessageDto
    {
        public enum MessageType { FOLLOWERS, CLUB }
        public long UserId { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public long ClubId { get; set; }
        public List<ResourceDto> Resources { get; set; }
    }
}
