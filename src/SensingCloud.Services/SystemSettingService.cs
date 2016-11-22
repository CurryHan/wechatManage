using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sensing.Data.Repository;
using Sensing.Entities.SystemSettings;

namespace SensingCloud.Services
{
    public interface ISystemSettingService
    {
        bool NeedApprove(string name);
        PlatformNotification GetPlatformNotification();
    }
    public class SystemSettingService : ISystemSettingService
    {
        public SystemSettingService(ISystemSettingRepository systemSettingRepository, IPlatformNotificationRepository platformNotification)
        {
            this._systemSettingRepository = systemSettingRepository;
            this._platformNotificationRepository = platformNotification;
        }


        private ISystemSettingRepository _systemSettingRepository;
        private IPlatformNotificationRepository _platformNotificationRepository;

        public bool NeedApprove(string name)
        {
            return _systemSettingRepository.NeedApprove(name);
        }
        public PlatformNotification GetPlatformNotification()
        {
            return _platformNotificationRepository.GetPlatformNotification();
        }
    }
}
