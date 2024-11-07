using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
       // Notification Create(long userId,long resourceId);
        List<Notification> GetUnreadByUser(long userId);
        List<Notification> GetAllByUser(long userId);
    }
}
