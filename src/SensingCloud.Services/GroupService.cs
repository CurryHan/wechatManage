using Sensing.Data.Infrastructure;
using Sensing.Data.Repository;
using Sensing.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingCloud.Services
{
    public interface IGroupService
    {
        void AddGroup(Group group, Group parentGroup);
        //void AddGroup(Group group);
        IEnumerable<Group> GetAll();
        List<Group> GetGroupTreeData(Group group);

        Group FindById(int id);
        void UpdateGroup(Group group);
        List<Group> GetGroupInclude(int currentGroupID, params GroupEnum[] groupTypes);

        void DeleteGroup(Group group);
    }

    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IGroupRepository _groupRepository;
        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            this._groupRepository = groupRepository;
            this._unitOfWork = unitOfWork;
        }

        //public void AddGroup(Group group)
        //{
        //    _groupRepository.Add(group);
        //}

        public void UpdateGroup(Group group)
        {
            _groupRepository.Update(group);
        }

        public void AddGroup(Group group, Group parentGroup)
        {
            group.ParentGroup = parentGroup;
            _groupRepository.Add(group);
        }

        public IEnumerable<Group> GetAll()
        {
            return _groupRepository.GetAll();
        }

        public List<Group> GetGroupInclude(int currentGroupID, params GroupEnum[] groupTypes)
        {
            List<Group> groupList = new List<Group>();
            var group = FindById(currentGroupID);
            if (groupTypes == null || groupTypes.Length == 0)
            {
                groupList = GetGroupTreeData(group);
                return groupList;
            }
            else
            {
                groupList = GetGroupTreeData(group).Where(p => groupTypes.Contains(p.GroupType)).ToList();
                return groupList;
            }
            //return null;
        }

        public List<Group> GetGroupTreeData(Group group)
        {
            List<Group> groupList = new List<Group>();
            GetGroupInfoByParent(group, groupList);
            var list = groupList.OrderBy(g => g.GroupType).ThenByDescending(g => g.Created).ToList();
            return list;
        }


        public Group FindById(int id)
        {
            return _groupRepository.GetById(id);
        }

        private void GetGroupInfoByParent(Group group, List<Group> groupList)
        {
            var _tempgroup = _groupRepository.GetInclude(group);
            if (_tempgroup != null)
            {
                groupList.Add(_tempgroup);
                foreach (var item in _tempgroup.Children)
                {
                    GetGroupInfoByParent(item, groupList);
                }
            }
        }

        public void DeleteGroup(Group group)
        {
            _groupRepository.Delete(group);
            _unitOfWork.DataContext.Database.ExecuteSqlCommand("uspDeleteGroup @iGroupID", new SqlParameter("@iGroupID", group.Id));
        }

    }
}
