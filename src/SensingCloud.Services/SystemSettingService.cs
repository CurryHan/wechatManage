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

        void Add(PlatformNotification notice);

        void Update(PlatformNotification notice);
    }
    public class SystemSettingService : ISystemSettingService
    {
        public SystemSettingService(IPlatformNotificationRepository platformNotification)
        {
            //this._systemSettingRepository = systemSettingRepository;
            this._platformNotificationRepository = platformNotification;
        }


        //private ISystemSettingRepository _systemSettingRepository;
        private IPlatformNotificationRepository _platformNotificationRepository;

        public bool NeedApprove(string name)
        {
            return true;
        }
        public PlatformNotification GetPlatformNotification()
        {
            return _platformNotificationRepository.GetPlatformNotification();
        }

        public void Add(PlatformNotification notice)
        {
            _platformNotificationRepository.Add(notice);
        }

        public void Update(PlatformNotification notice)
        {
            _platformNotificationRepository.Update(notice);
        }
    }
}
