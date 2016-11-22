using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sensing.Data.Infrastructure;
using Sensing.Entities;
using Sensing.Entities.Users;
using SensingCloud;
using SensingCloud.Authorization;
using SensingCloud.Models;
using SensingCloud.Services;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace SensingCloud.Controllers
{
    [Authorize]
    public class UserController : LanguageController
    {
        private readonly IUserService _userService;
        private IUnitOfWork _unitOfWork;
        private IGroupService _groupService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ApplicationUserManager userManager, IUserService userService,IGroupService groupService,RoleManager<IdentityRole> roleManager ,IUnitOfWork unitOfWork)
        {
            UserManager = userManager;
            this._userService = userService;
            this._unitOfWork = unitOfWork;
            this._groupService = groupService;
            this._roleManager = roleManager;

        }

        public ApplicationUserManager UserManager { get; private set; }

        //[Authorize(Roles = (RoleString.Admin + "," + RoleString.Manager))]
        //[ClaimsAuthorize(Claims = ("UserManage"))]
        public ActionResult Index(string query = "", int pageIndex = 1)
        {
            var groups = _groupService.GetGroupTreeData(CurrentGroup);
            var currentUserId = User.Identity.GetUserId();
            var userViewModels = UserManager.Users.OrderBy(u=>u.Group.GroupType).ThenBy(u=>u.UserName).ToList().
                Where(p => groups.Any(g => g.Id == p.GroupId) && p.Id != currentUserId)
                .Select(u => new UserViewModel
            {
                UserName = u.UserName,
                Id = u.Id,
                PhoneNumber = u.PhoneNumber,
                Company = u.CompanyName,
                Gender = u.Gender,
                IsActived = u.IsActived,
                Group=u.Group
            }).ToList();
             
            foreach (var userViewModel in userViewModels)
            {
                userViewModel.Role = UserManager.GetRoles(userViewModel.Id).Aggregate("",(total,part) => total + part);
            }
            ViewBag.query = query;

            if (!string.IsNullOrEmpty(query))
            {
                userViewModels = userViewModels.Where(user => string.Format("{0}-{1}-{2}", user.Company, user.Role, user.UserName).ToLower().Contains(query.ToLower())).ToList();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserList",userViewModels.ToPagedList(pageIndex, 20));
            }
            return View(userViewModels.ToPagedList(pageIndex, 20));
        }


        // GET: /Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Movie movie = db.Movies.Find(id);
            //if (movie == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        [Authorize(Roles = (RoleString.Admin + "," + RoleString.Manager))]
        [HttpGet]
        public ActionResult NewUser()
        {
            return PartialView("_CreateUserDialog", GetUserViewModel());
        }

        [Authorize(Roles = (RoleString.Admin + "," + RoleString.Manager))]
        [HttpPost]
        public ActionResult CreateUser(CreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = user.UserName, 
                    CompanyName = user.Company,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    PasswordHash = "1qaz@WSX",
                    EmailConfirmed = true,
                    AvatarUrl = "",
                };
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase avatar = Request.Files["avatar"];
                    if (avatar.ContentLength > 0)
                    {
                        var fileName =DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetFileName(avatar.FileName);
                        var path = Path.Combine(Server.MapPath("~/Upload/Avatars"), fileName);
                        avatar.SaveAs(path);
                        appUser.AvatarUrl = Path.Combine("/Upload/Avatars", fileName);
                    }
                }
                if (UserManager.FindByName(appUser.UserName) == null)
                {
                    if (user.SelectedGroupId == -1)
                    {
                        var newGroup = new Group { DisplayName = user.NewGroupName };
                        appUser.Group = newGroup;
                    }
                    else
                    {
                        appUser.GroupId = user.SelectedGroupId;
                    }


                    IdentityResult result =  UserManager.Create(appUser);
                    if (result.Succeeded)
                    {
                        user.Id = appUser.Id;
                        UserManager.AddToRole(appUser.Id, user.SelectRoleName);
                        ModelState.Clear();
                        ViewBag.Success = true;
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            if (error.StartsWith("Email"))
                            {
                                ModelState.AddModelError("Email", error);
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "创建失败，用户名已存在");
                }
            }

            return PartialView("_CreateUserDialog", GetUserViewModel());
        }

        private CreateUserViewModel GetUserViewModel()
        {
            CreateUserViewModel model = new CreateUserViewModel();
            var groups = _groupService.GetGroupTreeData(CurrentGroup);
            //set group Dropdown 
            model.GroupNames = _groupService.GetAll().Where(p=>groups.Any(g=>g.Id==p.Id))
                                             .Select(g => new GroupName
                                             {
                                                 Id = g.Id,
                                                 Name = g.DisplayName
                                             }).ToList();
            //set role drowdown
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList(); 
            //if (User.IsInRole(RoleString.Admin))
            //{
            //    allRoles.Remove(RoleString.Admin);
            //}
            //else if (User.IsInRole(RoleString.Manager))
            //{
            //    allRoles.Remove(RoleString.Admin);
            //    allRoles.Remove(RoleString.Manager);
            //}
            model.RoleNames = allRoles;
            ViewBag.CurrentGroupID = CurrentGroupID;
            return model;
        }


        [HttpGet]
        public ActionResult EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserViewModel viewModel = new UserViewModel();
            viewModel.UserName = user.UserName;
            viewModel.Company = user.CompanyName;
            viewModel.PhoneNumber = user.PhoneNumber;
            viewModel.Role = UserManager.GetRoles(user.Id)[0];
            viewModel.Group = user.Group;
            return PartialView("_EditUserDialog", viewModel);
        }

        [HttpPut]
        public ActionResult UpdateUser([Bind(Include = "UserName,Role,Company,PhoneNumber")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = UserManager.FindByName(userViewModel.UserName);
                if (appUser != null)
                {
                    appUser.PhoneNumber = userViewModel.PhoneNumber;
                    appUser.CompanyName = userViewModel.Company;
                    var oldRole = UserManager.GetRoles(appUser.Id)[0];
                    userViewModel.Id = appUser.Id;
                    if (userViewModel.Role != oldRole)
                    {
                        UserManager.RemoveFromRole(appUser.Id, oldRole);
                        UserManager.AddToRole(appUser.Id, userViewModel.Role);
                    }
                    UserManager.Update(appUser);
                    return PartialView("_UserListItem", userViewModel);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
        }

        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           ApplicationUser user =  UserManager.FindById(id);
           if (user == null)
           {
               return HttpNotFound();
           }
           UserViewModel viewModel = new UserViewModel();
           viewModel.Id = user.Id;
           viewModel.UserName = user.UserName;
           viewModel.Role = UserManager.GetRoles(user.Id)[0];
            viewModel.Group = user.Group;
           return PartialView("_DeleteUserDialog",viewModel);
        }

        [HttpDelete, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user =  UserManager.FindById(id);
            if (user.UserName == User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "不能删除自身");
            }
            UserManager.Delete(user);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult LockUser(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            user.IsActived = false;
            IdentityResult  result = UserManager.Update(user);
            if (!result.Succeeded)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var vm = AutoMapper.Mapper.Map<UserViewModel>(user);
            vm.Role = UserManager.GetRoles(user.Id).Aggregate("", (total, part) => total + part);
            return PartialView("_UserListItem", vm);
       
        }

        [HttpPost]
        public ActionResult UnLockUser(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            user.IsActived = true;
            IdentityResult result = UserManager.Update(user);
            if (!result.Succeeded)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = AutoMapper.Mapper.Map<UserViewModel>(user);
            vm.Role = UserManager.GetRoles(user.Id).Aggregate("", (total, part) => total + part);
            return PartialView("_UserListItem", vm);
        }

        public ActionResult UserProfile()
        {
            var currentUser = UserManager.FindByName(User.Identity.Name);
            return View(currentUser);
        }


        [HttpPost]
        public ActionResult Update(string name,string value )
        {
            var user = UserManager.FindByName(User.Identity.Name);
            switch (name)
            {
                case  "Name":
                    user.LastName = value;
                    _userService.AddUserActivity(user.Id, "修改了姓名");
                    break;
                case "Company":
                    user.CompanyName = value;
                    _userService.AddUserActivity(user.Id, "修改了公司名");
                    break;
                case "Email":
                    user.Email = value;
                    _userService.AddUserActivity(user.Id, "修改了邮箱");
                    break;
                case "PhoneNumber":
                    user.PhoneNumber = value;
                    _userService.AddUserActivity(user.Id, "修改了电话");
                    break;
                case "Male":
                    user.Gender = value == "1" ? "男" : "女";
                    _userService.AddUserActivity(user.Id, "修改了性别");
                    break;
            }
            if(Request.Files.Count > 0)
            {
                HttpPostedFileBase avatar = Request.Files["avatar"];
                if (avatar.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(avatar.FileName);
                    fileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss.") + Path.GetExtension(fileName);

                    string avatarsDir = Server.MapPath("~/Upload/Avatars");
                    if (!Directory.Exists(avatarsDir)) Directory.CreateDirectory(avatarsDir);

                    var path = Path.Combine(avatarsDir, fileName);
                    avatar.SaveAs(path);

                    user.AvatarUrl = Path.Combine("/Upload/Avatars", fileName);
                    _userService.AddUserActivity(user.Id, "修改了头像");
                }
            }

            IdentityResult result =  UserManager.Update(user);
            _unitOfWork.Commit();
            if(name == "avatar")
            {
                return Json(new  { AvatarUrl = user.AvatarUrl });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView("_ChangePasswordForm");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangePasswordForm");
            }
            var curUser = UserManager.FindByName(User.Identity.Name);
            if (curUser.PasswordHash != model.OldPassword)
            {
                ModelState.AddModelError("OldPassword", "原密码不对");
                return PartialView("_ChangePasswordForm");
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return PartialView("_ChangePasswordForm");
            }
            curUser.PasswordHash = model.NewPassword;
            IdentityResult result = UserManager.Update(curUser);
            ViewBag.Success = true;
            _userService.AddUserActivity(User.Identity.GetUserId(), "修改密码");
            return PartialView("_ChangePasswordForm");
        }

        [HttpGet]
        public ActionResult AdminResetPassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return PartialView("_ResetPasswordDialog",
                new AdminResetPasswordViewModel
                {
                    UserName = user.UserName
                    , Company = user.CompanyName
                });
        }

        [HttpPost]
        public ActionResult AdminResetPassword(AdminResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var curUser = UserManager.FindByName(model.UserName);
                curUser.CompanyName = model.Company;
                curUser.PasswordHash = model.NewPassword;
                UserManager.Update(curUser);
                ViewBag.Success = true;
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult UserActivity()
        {
            return PartialView("_UserActivity", 
                _userService.GetAllUserActivity()
                             .OrderByDescending(t => t.CreatedTime)
                              .Take(20));
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                var currentUser = UserManager.FindByName(User.Identity.Name);
                _groupService.AddGroup(group, currentUser.Group);
                _unitOfWork.Commit();
                
            }
            return View();
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }


    }
}