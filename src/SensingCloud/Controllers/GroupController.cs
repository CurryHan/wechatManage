using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sensing.Data;
using Sensing.Data.Infrastructure;
using Sensing.Entities;
using Sensing.Entities.Users;
using SensingCloud.Helpers;
using SensingCloud.Services;
using Webdiyer.WebControls.Mvc;
using System.IO;
using SensingCloud.Authorization;

namespace SensingCloud.Controllers
{
  
    public class GroupController : LanguageController
    {
        private readonly IGroupService _groupService;
        private readonly IUnitOfWork _unitOfwork;
        private readonly IUserService _userSvc;

        public GroupController(IGroupService groupService, IUnitOfWork unitofWork,IUserService userSvc)
        {
            this._groupService = groupService;
            this._unitOfwork = unitofWork;
            this._userSvc = userSvc;
        }

        // GET: Group
        public ActionResult Index(string query = "", string mode = "list", int pageIndex = 1)
        {
            int pageSize = 20;
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.Mode = mode;
            var list = new List<Group>();
            var loggeduser = GetLoggedUserInfo();
            if (loggeduser != null)
            {
                list = _groupService.GetGroupTreeData(_groupService.FindById(CurrentGroupID));
                if (!string.IsNullOrEmpty(query))
                {
                    list = list.Where(g => g.Deleted == false && g.DisplayName.Contains(query)).ToPagedList(pageIndex, pageSize);
                }
                else
                {
                    list = list.Where(g => g.Deleted == false).ToPagedList(pageIndex, pageSize);
                }
            }
            return View(list);
        }

        //[HttpGet]
        //public ActionResult GetGroupList(string query = "", string mode = "list", int pageIndex = 1)
        //{
        //    int pageSize = 20;
        //    ViewBag.pageIndex = pageIndex;
        //    ViewBag.pageSize = pageSize;
        //    ViewBag.Mode = mode;
        //    var list = new List<Group>();
        //    var loggeduser = GetLoggedUserInfo();
        //    if (loggeduser != null)
        //    {
        //        list = _groupService.GetGroupTreeData(_groupService.FindById(CurrentGroupID));
        //        if (!string.IsNullOrEmpty(query))
        //        {
        //            list = list.Where(g => g.Deleted == false && g.DisplayName.Contains(query)).ToPagedList(pageIndex, pageSize);
        //        }
        //        else
        //        {
        //            list = list.Where(g => g.Deleted == false).ToPagedList(pageIndex, pageSize);
        //        }
        //    }
        //    return PartialView("_GroupListPartial", list);
        //}
        [GroupAuthorize(Roles = (RoleString.Editor + "," + RoleString.Admin), Groups = GroupTypeString.SuperLevel + "," + GroupTypeString.SI)]
        [HttpGet]
        public ActionResult Create()
        {
            //var loggeduser = GetLoggedUserInfo();
            var list = new List<Group>();

            list = _groupService.GetGroupTreeData(CurrentGroup).Where(g => g.Deleted == false && (g.Creator == User.Identity.Name || CurrentGroupID == g.Id)).ToList();
            List<SelectListItem> listType = new List<SelectListItem>();

            if (CurrentGroup.GroupType == GroupEnum.SuperLevel)
            {
                if(User.IsInRole(RoleString.Admin))
                {
                    listType.Add(new SelectListItem
                    {
                        Value = ((int)GroupEnum.SI).ToString(),
                        Text = GroupEnum.SI.ToName()
                    });
                }

                listType.Add(new SelectListItem
                {
                    Value = ((int)GroupEnum.Brand).ToString(),
                    Text = GroupEnum.Brand.ToName()
                });
                listType.Add(new SelectListItem
                {
                    Value = ((int)GroupEnum.Store).ToString(),
                    Text = GroupEnum.Store.ToName()
                });
            }
            else if (CurrentGroup.GroupType == GroupEnum.SI)
            {
                listType.Add(new SelectListItem
                {
                    Value = ((int)GroupEnum.Brand).ToString(),
                    Text = GroupEnum.Brand.ToName()
                });
                listType.Add(new SelectListItem
                {
                    Value = ((int)GroupEnum.Store).ToString(),
                    Text = GroupEnum.Store.ToName()
                });
            }
            else if (CurrentGroup.GroupType==GroupEnum.Brand)
            {
                listType.Add(new SelectListItem
                {
                    Value = ((int)GroupEnum.Store).ToString(),
                    Text = GroupEnum.Store.ToName()
                });
            }
            ViewBag.GroupType = listType;
            ViewBag.GroupList = list;

            //            ViewBag.GroupTagValues = _mTagValueService.GetGroupAllTagValue(ConstConfig.GroupTag).ToList()
            //.Select(t => new SelectListItem { Text = t.Value, Value = t.Id.ToString() });

            return PartialView("_CreatePartial");

        }
        [GroupAuthorize(Roles = (RoleString.Editor + "," + RoleString.Admin), Groups = GroupTypeString.SuperLevel + "," + GroupTypeString.SI)]
        [HttpPost]
        public ActionResult Create(Group group)
        {
            if (group.GroupType == GroupEnum.Brand && string.IsNullOrEmpty(group.StoreId)) ModelState.AddModelError(string.Empty,"新建品牌商时,StoreId不能为空");
            if (ModelState.IsValid)
            {
                var _tempId = Request.Params["parentsGroupselect"];
                var parentGroup = new Group();
                if (!string.IsNullOrEmpty(_tempId))
                {
                    parentGroup = _groupService.FindById(int.Parse(_tempId));
                    var grouplist = _groupService.GetGroupTreeData(parentGroup).Where(g => g.DisplayName == group.DisplayName && g.Deleted == false).ToList();
                    if (grouplist.Count > 0)
                    {
                        ModelState.AddModelError("DisplayName", Resources.Resources.GroupName + Resources.Resources.AlreadyHave);
                        return PartialView("_CreatePartial", group);
                    }

                    //图片上传处理
                    if (Request.Files.Count > 0)
                    {
                        foreach (string file in Request.Files)
                        {
                            HttpPostedFileBase avatar = Request.Files[file];
                            switch (file)
                            {
                                case "logoImg":
                                    var _logoImg = SaveUploadedFile(avatar, string.Format(ConstConfig.Customer_Group_Path, 0), file);//"~/Upload/Group/Logo", file);
                                    if (!string.IsNullOrEmpty(_logoImg)) group.LogoUrl = _logoImg;
                                    break;
                                case "Image01":
                                    var _Image01 = SaveUploadedFile(avatar, string.Format(ConstConfig.Customer_Group_Path, 0), file);//"~/Upload/Group/Image", file);
                                    if (!string.IsNullOrEmpty(_Image01)) group.Image01Url = _Image01;
                                    break;
                                case "Image02":
                                    var _Image02 = SaveUploadedFile(avatar, string.Format(ConstConfig.Customer_Group_Path, 0), file);//"~/Upload/Group/Image", file);
                                    if (!string.IsNullOrEmpty(_Image02)) group.Image02Url = _Image02;
                                    break;
                                case "Image03":
                                    var _Image03 = SaveUploadedFile(avatar, string.Format(ConstConfig.Customer_Group_Path, 0), file);// "~/Upload/Group/Image", file);
                                    if (!string.IsNullOrEmpty(_Image03)) group.Image03Url = _Image03;
                                    break;
                                case "Image04":
                                    var _Image04 = SaveUploadedFile(avatar, string.Format(ConstConfig.Customer_Group_Path, 0), file);//"~/Upload/Group/Image", file);
                                    if (!string.IsNullOrEmpty(_Image04)) group.Image01Url = _Image04;
                                    break;
                            }
                        }
                    }

                    if (parentGroup != null)
                    {
                        group.Created = DateTime.Now;
                        group.Creator = User.Identity.Name;
                        group.Deleted = false;
                        group.Updater = User.Identity.Name;
                        group.LastUpdated = DateTime.Now;
                        group.SubscriptionKey = Guid.NewGuid().ToString("N");
                        _groupService.AddGroup(group, parentGroup);
                        _unitOfwork.Commit();
                        return Json(true);
                    }
                }
            }
            return Create();
        }

        [GroupAuthorize(Roles = (RoleString.Editor + "," + RoleString.Admin), Groups = GroupTypeString.SuperLevel + "," + GroupTypeString.SI)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var model = db.Groups.Include(g => g.Children).Include(g => g.ParentGroup).Where(g => g.Id == id).FirstOrDefault();
            var model = _groupService.FindById(id);
            if (model == null)
                return HttpNotFound();
            return PartialView("_EditPartial", model);
        }

        public void UpdateSubKeyWhenIsEmpty()
        {
            var groups=_groupService.GetAll();
            foreach (var item in groups)
            {
                if (string.IsNullOrEmpty(item.SubscriptionKey)) item.SubscriptionKey = Guid.NewGuid().ToString("N");
                _groupService.UpdateGroup(item);
            }
        }

        [GroupAuthorize(Roles = (RoleString.Editor + "," + RoleString.Admin), Groups = GroupTypeString.SuperLevel + "," + GroupTypeString.SI)]
        [HttpPost]
        public ActionResult Edit(Group group)
        {
            if (group.GroupType == GroupEnum.Brand && string.IsNullOrEmpty(group.StoreId)) ModelState.AddModelError(string.Empty, "新建品牌商时,StoreId不能为空");
            if (ModelState.IsValid)
            {
                //图片上传处理
                if (Request.Files.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase avatar = Request.Files[file];
                        switch (file)
                        {
                            case "logoImg":
                                var _logoImg = SaveUploadedFile(avatar, ConstConfig.GetCustomerGroupDataPath(group.Id), file);//"~/Upload/Group/Logo", file);
                                if (!string.IsNullOrEmpty(_logoImg)) group.LogoUrl = _logoImg;
                                break;
                            case "Image01":
                                var _Image01 = SaveUploadedFile(avatar, ConstConfig.GetCustomerGroupDataPath(group.Id), file);//"~/Upload/Group/Image", file);
                                if (!string.IsNullOrEmpty(_Image01)) group.Image01Url = _Image01;
                                break;
                            case "Image02":
                                var _Image02 = SaveUploadedFile(avatar, ConstConfig.GetCustomerGroupDataPath(group.Id), file);//"~/Upload/Group/Image", file);
                                if (!string.IsNullOrEmpty(_Image02)) group.Image02Url = _Image02;
                                break;
                            case "Image03":
                                var _Image03 = SaveUploadedFile(avatar, ConstConfig.GetCustomerGroupDataPath(group.Id), file);//"~/Upload/Group/Image", file);
                                if (!string.IsNullOrEmpty(_Image03)) group.Image03Url = _Image03;
                                break;
                            case "Image04":
                                var _Image04 = SaveUploadedFile(avatar, ConstConfig.GetCustomerGroupDataPath(group.Id), file);//"~/Upload/Group/Image", file);
                                if (!string.IsNullOrEmpty(_Image04)) group.Image04Url = _Image04;
                                break;
                        }
                    }
                }
                try
                {
                    group.Updater = User.Identity.Name;
                    group.LastUpdated = DateTime.Now;
                    _groupService.UpdateGroup(group);
                    _unitOfwork.Commit();
                    return Json(true);
                }
                catch(Exception ex)
                {
                    logger.Error(ex);
                    return Json(false);
                }

               
            }
            return PartialView("_EditPartial", group);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _groupService.FindById(id);
            if (model == null)
                return HttpNotFound();
            return PartialView("_DetailsPartial", model);
        }
        [GroupAuthorize(Roles = (RoleString.Editor + "," + RoleString.Admin), Groups = GroupTypeString.SuperLevel + "," + GroupTypeString.SI)]
        // 删除分组
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var delgroup = db.Groups.Include(g => g.Children).Include(g => g.ParentGroup).Where(g => g.Id == id).FirstOrDefault();
            var delgroup = _groupService.FindById(id.Value);
            if (delgroup != null)
            {
                var grouplist = _groupService.GetGroupTreeData(delgroup);
                    foreach (var item in grouplist)
                    {
                        _groupService.DeleteGroup(item);
                    }
                    //_unitOfwork.Commit();
                    //db.SaveChanges();
                    return Content("Ok");
            }
            return Content("No");

        }


        public ActionResult GetParentGroupByType(int targetGroupType)
        {
            List<Group> list = null;
            if (targetGroupType == (int)GroupEnum.SI)
                list = _groupService.GetGroupInclude(CurrentGroupID, GroupEnum.SuperLevel);
            else if (targetGroupType == (int)GroupEnum.Brand)
                list = _groupService.GetGroupInclude(CurrentGroupID, GroupEnum.SI);
            else if (targetGroupType == (int)GroupEnum.Store)
                list = _groupService.GetGroupInclude(CurrentGroupID, GroupEnum.Brand);
            return Json(new { data = list.Select(p => new { value = p.Id, name = p.DisplayName }).ToList() }, JsonRequestBehavior.AllowGet);
        }

        private ApplicationUser GetLoggedUserInfo()
        {
            var userName = User.Identity.Name;
            var _temp = _userSvc.GetUserbyName(userName);
            if (_temp != null)
            {
                _temp.Group = _groupService.FindById(_temp.GroupId.Value);
                return _temp;
            }
            return null;
        }

    }
}