using Sensing.Data.Infrastructure;
using Sensing.Data.Repository;
using Sensing.Entities.Versions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensingCloud.Services
{
    public interface ISoftwareVersionService
    {
        Task<IEnumerable<SoftwareClientDetails>> GetAllAsync();
        SoftwareClientDetails GetById(int id);
        void Add(SoftwareClientDetails camera);
        void Delete(SoftwareClientDetails camera);
        void Update(SoftwareClientDetails camera);

        IEnumerable<SoftwareClientDetails> GetUpperVesionByIdAsync(int id);
        void Save();
    }
    public class SoftwareUpdateService : ISoftwareVersionService
    {
        private readonly ISoftwareUpdateRepository _softwareVersionRepository;
        private readonly IUnitOfWork unitOfWork;

        public SoftwareUpdateService(ISoftwareUpdateRepository cameraRepository, IUnitOfWork unitOfWork)
        {
            this._softwareVersionRepository = cameraRepository;
            this.unitOfWork = unitOfWork;

        }
        public async Task<IEnumerable<SoftwareClientDetails>> GetAllAsync()
        {
            return await _softwareVersionRepository.GetAllAsync();
        }


        public IEnumerable<SoftwareClientDetails> GetUpperVesionByIdAsync(int id)
        {
            return _softwareVersionRepository.GetUpperVersionByIdAsync(id);
        }
        public SoftwareClientDetails GetById(int id)
        {
            return _softwareVersionRepository.GetById(id);
        }

        public void Add(SoftwareClientDetails camera)
        {
            _softwareVersionRepository.Add(camera);
        }


        public void Delete(SoftwareClientDetails camera)
        {
            _softwareVersionRepository.Delete(camera);
        }


        public void Update(SoftwareClientDetails camera)
        {
            _softwareVersionRepository.Update(camera);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}

