using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
    
namespace Resources {
        public class Resources {
            private static IResourceProvider resourceProvider = new XmlResourceProvider();

                
        /// <summary>O2O解决方案</summary>
        public static string SensingTitle {
               get {
                   return (string) resourceProvider.GetResource("SensingTitle", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>多语言测试</summary>
        public static string TestingValue {
               get {
                   return (string) resourceProvider.GetResource("TestingValue", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>新建组</summary>
        public static string CreateGroup {
               get {
                   return (string) resourceProvider.GetResource("CreateGroup", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>组名</summary>
        public static string GroupName {
               get {
                   return (string) resourceProvider.GetResource("GroupName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备名已存在.</summary>
        public static string DeviceNameAlreadyExists {
               get {
                   return (string) resourceProvider.GetResource("DeviceNameAlreadyExists", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>选择分组</summary>
        public static string ChoiceGroup {
               get {
                   return (string) resourceProvider.GetResource("ChoiceGroup", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>保存</summary>
        public static string Save {
               get {
                   return (string) resourceProvider.GetResource("Save", CultureInfo.CurrentUICulture.Name);
               }
            }

        /// <summary>复制</summary>
        public static string Copy
        {
            get
            {
                return (string)resourceProvider.GetResource("Copy", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary> 确认</summary>
        public static string Confirm {
               get {
                   return (string) resourceProvider.GetResource("Confirm", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>检索</summary>
        public static string Search {
               get {
                   return (string) resourceProvider.GetResource("Search", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>编辑</summary>
        public static string Edit {
               get {
                   return (string) resourceProvider.GetResource("Edit", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>删除</summary>
        public static string Delete {
               get {
                   return (string) resourceProvider.GetResource("Delete", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>详细</summary>
        public static string Details {
               get {
                   return (string) resourceProvider.GetResource("Details", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>添加</summary>
        public static string Create {
               get {
                   return (string) resourceProvider.GetResource("Create", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>返回列表</summary>
        public static string ReturnToList {
               get {
                   return (string) resourceProvider.GetResource("ReturnToList", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>关于</summary>
        public static string About {
               get {
                   return (string) resourceProvider.GetResource("About", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>操作</summary>
        public static string Action {
               get {
                   return (string) resourceProvider.GetResource("Action", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>创建者</summary>
        public static string Creator {
               get {
                   return (string) resourceProvider.GetResource("Creator", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>创建日期</summary>
        public static string Created {
               get {
                   return (string) resourceProvider.GetResource("Created", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>修改者</summary>
        public static string Updater {
               get {
                   return (string) resourceProvider.GetResource("Updater", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>修改日期</summary>
        public static string LastUpdated {
               get {
                   return (string) resourceProvider.GetResource("LastUpdated", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>所属分组</summary>
        public static string ParentGroup {
               get {
                   return (string) resourceProvider.GetResource("ParentGroup", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>下页</summary>
        public static string NextPage {
               get {
                   return (string) resourceProvider.GetResource("NextPage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>上页</summary>
        public static string LastPage {
               get {
                   return (string) resourceProvider.GetResource("LastPage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>首页</summary>
        public static string HomePage {
               get {
                   return (string) resourceProvider.GetResource("HomePage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>尾页</summary>
        public static string EndPage {
               get {
                   return (string) resourceProvider.GetResource("EndPage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>名称</summary>
        public static string Name {
               get {
                   return (string) resourceProvider.GetResource("Name", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>状态</summary>
        public static string Status {
               get {
                   return (string) resourceProvider.GetResource("Status", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>IP地址格式不符</summary>
        public static string InvalidIP {
               get {
                   return (string) resourceProvider.GetResource("InvalidIP", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>Mac地址格式不符</summary>
        public static string InvalidMac {
               get {
                   return (string) resourceProvider.GetResource("InvalidMac", CultureInfo.CurrentUICulture.Name);
               }
            }


        public static string InvalidUrl {
            get
            {
                return (string)resourceProvider.GetResource("InvalidUrl", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string InvalidPhone {
            get {
                return (string)resourceProvider.GetResource("InvalidPhone", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>商品</summary>
        public static string Product {
               get {
                   return (string) resourceProvider.GetResource("Product", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>名称</summary>
        public static string PName {
               get {
                   return (string) resourceProvider.GetResource("PName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>品类管理</summary>
        public static string CategoryManager {
               get {
                   return (string) resourceProvider.GetResource("CategoryManager", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品管理</summary>
        public static string ProductManager {
               get {
                   return (string) resourceProvider.GetResource("ProductManager", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>分组管理</summary>
        public static string GroupManager {
               get {
                   return (string) resourceProvider.GetResource("GroupManager", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>添加商品库存</summary>
        public static string AddProductInventory {
               get {
                   return (string) resourceProvider.GetResource("AddProductInventory", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品绑定到分类</summary>
        public static string ProductToCategory {
               get {
                   return (string) resourceProvider.GetResource("ProductToCategory", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>添加品类</summary>
        public static string CreateCategory {
               get {
                   return (string) resourceProvider.GetResource("CreateCategory", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>品类详细信息</summary>
        public static string CategoryDetails {
               get {
                   return (string) resourceProvider.GetResource("CategoryDetails", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>编辑品类</summary>
        public static string CategoryEdit {
               get {
                   return (string) resourceProvider.GetResource("CategoryEdit", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>添加商品</summary>
        public static string CreateProduct {
               get {
                   return (string) resourceProvider.GetResource("CreateProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品详细信息</summary>
        public static string ProductDetails {
               get {
                   return (string) resourceProvider.GetResource("ProductDetails", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>编辑商品</summary>
        public static string ProductEdit {
               get {
                   return (string) resourceProvider.GetResource("ProductEdit", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定删除？</summary>
        public static string ConfirmDelete {
               get {
                   return (string) resourceProvider.GetResource("ConfirmDelete", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定申请商品下线吗？</summary>
        public static string ConfirmApplyOffline {
               get {
                   return (string) resourceProvider.GetResource("ConfirmApplyOffline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定申请商品上线吗？</summary>
        public static string ConfirmApplyOnline {
               get {
                   return (string) resourceProvider.GetResource("ConfirmApplyOnline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定取消申请吗？</summary>
        public static string ConfirmCancel {
               get {
                   return (string) resourceProvider.GetResource("ConfirmCancel", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>请选择数据!</summary>
        public static string ConfirmChoiceData {
               get {
                   return (string) resourceProvider.GetResource("ConfirmChoiceData", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定同意商品下线吗？</summary>
        public static string ConfirmAgreeOffline {
               get {
                   return (string) resourceProvider.GetResource("ConfirmAgreeOffline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定同意商品上线吗？</summary>
        public static string ConfirmAgreeOnline {
               get {
                   return (string) resourceProvider.GetResource("ConfirmAgreeOnline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定拒绝商品下线吗？</summary>
        public static string ConfirmRefuseOffline {
               get {
                   return (string) resourceProvider.GetResource("ConfirmRefuseOffline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确定拒绝商品上线吗？</summary>
        public static string ConfirmRefuseOnline {
               get {
                   return (string) resourceProvider.GetResource("ConfirmRefuseOnline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>确认放弃添加新商品？</summary>
        public static string ConfirmGiveupNewProduct {
               get {
                   return (string) resourceProvider.GetResource("ConfirmGiveupNewProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>操作失败!</summary>
        public static string UseFailed {
               get {
                   return (string) resourceProvider.GetResource("UseFailed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>该分组内子类信息太多，请先清除改组子类再删除该组！</summary>
        public static string CannotDeleteGroup {
               get {
                   return (string) resourceProvider.GetResource("CannotDeleteGroup", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>修改成功.</summary>
        public static string UpdateSuccess {
               get {
                   return (string) resourceProvider.GetResource("UpdateSuccess", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>创建成功.</summary>
        public static string CreateSuccess {
               get {
                   return (string) resourceProvider.GetResource("CreateSuccess", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>不能为空.</summary>
        public static string NotNULL {
               get {
                   return (string) resourceProvider.GetResource("NotNULL", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>已存在.</summary>
        public static string AlreadyHave {
               get {
                   return (string) resourceProvider.GetResource("AlreadyHave", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>性别</summary>
        public static string Sex {
               get {
                   return (string) resourceProvider.GetResource("Sex", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品状态</summary>
        public static string AuditStatus {
               get {
                   return (string) resourceProvider.GetResource("AuditStatus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>品牌</summary>
        public static string Brand {
               get {
                   return (string) resourceProvider.GetResource("Brand", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>大分类</summary>
        public static string LargeClass {
               get {
                   return (string) resourceProvider.GetResource("LargeClass", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>小分类</summary>
        public static string SubClass {
               get {
                   return (string) resourceProvider.GetResource("SubClass", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>款式</summary>
        public static string Style {
               get {
                   return (string) resourceProvider.GetResource("Style", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>版型</summary>
        public static string Model {
               get {
                   return (string) resourceProvider.GetResource("Model", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>面料</summary>
        public static string Material {
               get {
                   return (string) resourceProvider.GetResource("Material", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>风格</summary>
        public static string Fashion {
               get {
                   return (string) resourceProvider.GetResource("Fashion", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>潮流发布</summary>
        public static string Trend {
               get {
                   return (string) resourceProvider.GetResource("Trend", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品SKU</summary>
        public static string Sku {
               get {
                   return (string) resourceProvider.GetResource("Sku", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>货号Spu</summary>
        public static string Spu {
               get {
                   return (string) resourceProvider.GetResource("Spu", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>销售价</summary>
        public static string Price {
               get {
                   return (string) resourceProvider.GetResource("Price", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>吊牌价</summary>
        public static string ListedPrice {
               get {
                   return (string) resourceProvider.GetResource("ListedPrice", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>色号</summary>
        public static string ColorId {
               get {
                   return (string) resourceProvider.GetResource("ColorId", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>颜色</summary>
        public static string ColorName {
               get {
                   return (string) resourceProvider.GetResource("ColorName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>上架日期</summary>
        public static string OnsaleDate {
               get {
                   return (string) resourceProvider.GetResource("OnsaleDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>尺码参照</summary>
        public static string SizeDescription {
               get {
                   return (string) resourceProvider.GetResource("SizeDescription", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>描述</summary>
        public static string Description {
               get {
                   return (string) resourceProvider.GetResource("Description", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>产品保养</summary>
        public static string Maintain {
               get {
                   return (string) resourceProvider.GetResource("Maintain", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品KeyWords</summary>
        public static string Keyword {
               get {
                   return (string) resourceProvider.GetResource("Keyword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品图</summary>
        public static string ProductImg {
               get {
                   return (string) resourceProvider.GetResource("ProductImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>模特图</summary>
        public static string ModelImg {
               get {
                   return (string) resourceProvider.GetResource("ModelImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>大图</summary>
        public static string BigImg {
               get {
                   return (string) resourceProvider.GetResource("BigImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>中图</summary>
        public static string MiddleImg {
               get {
                   return (string) resourceProvider.GetResource("MiddleImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>小图</summary>
        public static string SmallImg {
               get {
                   return (string) resourceProvider.GetResource("SmallImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>单品大图</summary>
        public static string BProductImg {
               get {
                   return (string) resourceProvider.GetResource("BProductImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>单品中图</summary>
        public static string MProductImg {
               get {
                   return (string) resourceProvider.GetResource("MProductImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>单品小图</summary>
        public static string SProductImg {
               get {
                   return (string) resourceProvider.GetResource("SProductImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>模特大图</summary>
        public static string BModelImg {
               get {
                   return (string) resourceProvider.GetResource("BModelImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>模特中图</summary>
        public static string MModelImg {
               get {
                   return (string) resourceProvider.GetResource("MModelImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>模特小图</summary>
        public static string SModelImg {
               get {
                   return (string) resourceProvider.GetResource("SModelImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品其他颜色</summary>
        public static string Colors {
               get {
                   return (string) resourceProvider.GetResource("Colors", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>最佳搭配</summary>
        public static string Matchs {
               get {
                   return (string) resourceProvider.GetResource("Matchs", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>其他可能喜欢</summary>
        public static string Favorites {
               get {
                   return (string) resourceProvider.GetResource("Favorites", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品库存</summary>
        public static string Inventorys {
               get {
                   return (string) resourceProvider.GetResource("Inventorys", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>备注</summary>
        public static string Remarks {
               get {
                   return (string) resourceProvider.GetResource("Remarks", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>尺码ID</summary>
        public static string Sizeid {
               get {
                   return (string) resourceProvider.GetResource("Sizeid", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>尺码名称</summary>
        public static string Sizename {
               get {
                   return (string) resourceProvider.GetResource("Sizename", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品库存量</summary>
        public static string InventoryNum {
               get {
                   return (string) resourceProvider.GetResource("InventoryNum", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>商品项编码</summary>
        public static string ItemNumber {
               get {
                   return (string) resourceProvider.GetResource("ItemNumber", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>国标码</summary>
        public static string ItemBarcode {
               get {
                   return (string) resourceProvider.GetResource("ItemBarcode", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>搭配效果大图</summary>
        public static string BImg {
               get {
                   return (string) resourceProvider.GetResource("BImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>搭配效果展示图</summary>
        public static string ShowImage {
               get {
                   return (string) resourceProvider.GetResource("ShowImage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>搭配效果中图</summary>
        public static string MImg {
               get {
                   return (string) resourceProvider.GetResource("MImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>搭配效果小图</summary>
        public static string SImg {
               get {
                   return (string) resourceProvider.GetResource("SImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>搭配列表</summary>
        public static string MatchItems {
               get {
                   return (string) resourceProvider.GetResource("MatchItems", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>理由</summary>
        public static string Reason {
               get {
                   return (string) resourceProvider.GetResource("Reason", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>介绍</summary>
        public static string Introduce {
               get {
                   return (string) resourceProvider.GetResource("Introduce", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>在线商品</summary>
        public static string OnlineProduct {
               get {
                   return (string) resourceProvider.GetResource("OnlineProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>在线商品一览表</summary>
        public static string OnlineProductTAB {
               get {
                   return (string) resourceProvider.GetResource("OnlineProductTAB", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>申请下线</summary>
        public static string ApplyOffLine {
               get {
                   return (string) resourceProvider.GetResource("ApplyOffLine", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键申请下线</summary>
        public static string OneKeyApplyOffLine {
               get {
                   return (string) resourceProvider.GetResource("OneKeyApplyOffLine", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>离线商品</summary>
        public static string OfflineProduct {
               get {
                   return (string) resourceProvider.GetResource("OfflineProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>离线商品一览表</summary>
        public static string OfflineProductTAB {
               get {
                   return (string) resourceProvider.GetResource("OfflineProductTAB", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>申请上线</summary>
        public static string ApplyOnLine {
               get {
                   return (string) resourceProvider.GetResource("ApplyOnLine", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键申请上线</summary>
        public static string OneKeyApplyOnLine {
               get {
                   return (string) resourceProvider.GetResource("OneKeyApplyOnLine", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>待审核商品</summary>
        public static string PendingProduct {
               get {
                   return (string) resourceProvider.GetResource("PendingProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>待审核商品一览表</summary>
        public static string PendingProductTAB {
               get {
                   return (string) resourceProvider.GetResource("PendingProductTAB", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>取消审核商品</summary>
        public static string CancelPendingProduct {
               get {
                   return (string) resourceProvider.GetResource("CancelPendingProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键取消待审核商品</summary>
        public static string OneKeyCancelPendingProduct {
               get {
                   return (string) resourceProvider.GetResource("OneKeyCancelPendingProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>申请下线商品</summary>
        public static string ApplyOffLineProduct {
               get {
                   return (string) resourceProvider.GetResource("ApplyOffLineProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>申请下线商品一览表</summary>
        public static string ApplyOffLineProductTAB {
               get {
                   return (string) resourceProvider.GetResource("ApplyOffLineProductTAB", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键下线商品</summary>
        public static string OneKeyOffLineProduct {
               get {
                   return (string) resourceProvider.GetResource("OneKeyOffLineProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>申请上线商品</summary>
        public static string ApplyOnLineProduct {
               get {
                   return (string) resourceProvider.GetResource("ApplyOnLineProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>申请上线商品一览表</summary>
        public static string ApplyOnLineProductTAB {
               get {
                   return (string) resourceProvider.GetResource("ApplyOnLineProductTAB", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键上线商品</summary>
        public static string OneKeyOnLineProduct {
               get {
                   return (string) resourceProvider.GetResource("OneKeyOnLineProduct", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>同意</summary>
        public static string AgreeApply {
               get {
                   return (string) resourceProvider.GetResource("AgreeApply", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>拒绝</summary>
        public static string RefuseApply {
               get {
                   return (string) resourceProvider.GetResource("RefuseApply", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>系统参数配置一览表</summary>
        public static string SystemSettingTAB {
               get {
                   return (string) resourceProvider.GetResource("SystemSettingTAB", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>平台通知</summary>
        public static string PlatNotifications {
               get {
                   return (string) resourceProvider.GetResource("PlatNotifications", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>邮箱通知配置</summary>
        public static string EmailSetting {
               get {
                   return (string) resourceProvider.GetResource("EmailSetting", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>短信通知接口</summary>
        public static string SMSSetting {
               get {
                   return (string) resourceProvider.GetResource("SMSSetting", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>审批流程</summary>
        public static string ApproveProcess {
               get {
                   return (string) resourceProvider.GetResource("ApproveProcess", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>终端内容更新</summary>
        public static string ClientContentUpdate {
               get {
                   return (string) resourceProvider.GetResource("ClientContentUpdate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>终端软件更新</summary>
        public static string ClientSoftUpdate {
               get {
                   return (string) resourceProvider.GetResource("ClientSoftUpdate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>新增软件更新包</summary>
        public static string AddSoftUpdatePackage {
               get {
                   return (string) resourceProvider.GetResource("AddSoftUpdatePackage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>软件更新包历史</summary>
        public static string SoftUpdatePackageListTBA {
               get {
                   return (string) resourceProvider.GetResource("SoftUpdatePackageListTBA", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>其他设置</summary>
        public static string OtherSet {
               get {
                   return (string) resourceProvider.GetResource("OtherSet", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>是否需要审批</summary>
        public static string NeedApprove {
               get {
                   return (string) resourceProvider.GetResource("NeedApprove", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>审批者角色</summary>
        public static string ApproverRole {
               get {
                   return (string) resourceProvider.GetResource("ApproverRole", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>服务器地址</summary>
        public static string ServerAddress {
               get {
                   return (string) resourceProvider.GetResource("ServerAddress", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>端口号</summary>
        public static string ServerPort {
               get {
                   return (string) resourceProvider.GetResource("ServerPort", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>服务用户名</summary>
        public static string EmailName {
               get {
                   return (string) resourceProvider.GetResource("EmailName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>服务密码</summary>
        public static string EmailPassword {
               get {
                   return (string) resourceProvider.GetResource("EmailPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>短信服务商url</summary>
        public static string SmsUrl {
               get {
                   return (string) resourceProvider.GetResource("SmsUrl", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>短信用户名</summary>
        public static string SmsUID {
               get {
                   return (string) resourceProvider.GetResource("SmsUID", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>短信密码</summary>
        public static string SmsPassword {
               get {
                   return (string) resourceProvider.GetResource("SmsPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>短信签名</summary>
        public static string MessageSignatrue {
               get {
                   return (string) resourceProvider.GetResource("MessageSignatrue", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>是否自动更新</summary>
        public static string IsAutoUpdate {
               get {
                   return (string) resourceProvider.GetResource("IsAutoUpdate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>自动更新频率</summary>
        public static string UpdateFrequency {
               get {
                   return (string) resourceProvider.GetResource("UpdateFrequency", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>定时更新时间</summary>
        public static string UpdateTime {
               get {
                   return (string) resourceProvider.GetResource("UpdateTime", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>定时更新</summary>
        public static string HourlyUpdate {
               get {
                   return (string) resourceProvider.GetResource("HourlyUpdate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>自动更新</summary>
        public static string AutoUpdate {
               get {
                   return (string) resourceProvider.GetResource("AutoUpdate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>版本号</summary>
        public static string SoftVersion {
               get {
                   return (string) resourceProvider.GetResource("SoftVersion", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>更新包地址</summary>
        public static string SoftPath {
               get {
                   return (string) resourceProvider.GetResource("SoftPath", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>分类</summary>
        public static string Category {
               get {
                   return (string) resourceProvider.GetResource("Category", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>排列序号</summary>
        public static string Priority {
               get {
                   return (string) resourceProvider.GetResource("Priority", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>类目图片</summary>
        public static string Image {
               get {
                   return (string) resourceProvider.GetResource("Image", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>后台展示图</summary>
        public static string WebShowImg {
               get {
                   return (string) resourceProvider.GetResource("WebShowImg", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>新建设备类型</summary>
        public static string CreateDeviceType {
               get {
                   return (string) resourceProvider.GetResource("CreateDeviceType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>更改设备类型</summary>
        public static string EditDeviceType {
               get {
                   return (string) resourceProvider.GetResource("EditDeviceType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>选择设备类型</summary>
        public static string SelectDeviceType {
               get {
                   return (string) resourceProvider.GetResource("SelectDeviceType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备名称</summary>
        public static string DeviceName {
               get {
                   return (string) resourceProvider.GetResource("DeviceName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备分类</summary>
        public static string DeviceType {
               get {
                   return (string) resourceProvider.GetResource("DeviceType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>审核状态</summary>
        public static string DeviceAuditStatus {
               get {
                   return (string) resourceProvider.GetResource("DeviceAuditStatus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备分组</summary>
        public static string DeviceGroup {
               get {
                   return (string) resourceProvider.GetResource("DeviceGroup", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备状态</summary>
        public static string DeviceStatus {
               get {
                   return (string) resourceProvider.GetResource("DeviceStatus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备详细</summary>
        public static string DeviceDetail {
               get {
                   return (string) resourceProvider.GetResource("DeviceDetail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>审核上线设备</summary>
        public static string AuditOnlineDevice {
               get {
                   return (string) resourceProvider.GetResource("AuditOnlineDevice", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>审核下线设备</summary>
        public static string AuditOfflineDevice {
               get {
                   return (string) resourceProvider.GetResource("AuditOfflineDevice", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键同意上线</summary>
        public static string OneKeyOnlineDevices {
               get {
                   return (string) resourceProvider.GetResource("OneKeyOnlineDevices", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键同意下线</summary>
        public static string OneKeyOfflineDevices {
               get {
                   return (string) resourceProvider.GetResource("OneKeyOfflineDevices", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备详情填加</summary>
        public static string AddDeviceDetail {
               get {
                   return (string) resourceProvider.GetResource("AddDeviceDetail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>楼宇</summary>
        public static string Building {
               get {
                   return (string) resourceProvider.GetResource("Building", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>楼层</summary>
        public static string Floor {
               get {
                   return (string) resourceProvider.GetResource("Floor", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备地址</summary>
        public static string DeviceAddress {
               get {
                   return (string) resourceProvider.GetResource("DeviceAddress", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>房间</summary>
        public static string Room {
               get {
                   return (string) resourceProvider.GetResource("Room", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备IP</summary>
        public static string DeviceIP {
               get {
                   return (string) resourceProvider.GetResource("DeviceIP", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>Mac地址</summary>
        public static string DeviceMac {
               get {
                   return (string) resourceProvider.GetResource("DeviceMac", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>相对静态图片坐标X</summary>
        public static string DeviceRelativeX {
               get {
                   return (string) resourceProvider.GetResource("DeviceRelativeX", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>相对静态图片坐标Y</summary>
        public static string DeviceRelativeY {
               get {
                   return (string) resourceProvider.GetResource("DeviceRelativeY", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>经度</summary>
        public static string Latitude {
               get {
                   return (string) resourceProvider.GetResource("Latitude", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>纬度</summary>
        public static string Longtitude {
               get {
                   return (string) resourceProvider.GetResource("Longtitude", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>坐标</summary>
        public static string Location {
               get {
                   return (string) resourceProvider.GetResource("Location", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>坐标X</summary>
        public static string LocationX {
               get {
                   return (string) resourceProvider.GetResource("LocationX", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>坐标Y</summary>
        public static string LocationY {
               get {
                   return (string) resourceProvider.GetResource("LocationY", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>添加设备</summary>
        public static string CreateDevice {
               get {
                   return (string) resourceProvider.GetResource("CreateDevice", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>线下设备</summary>
        public static string OfflineDevices {
               get {
                   return (string) resourceProvider.GetResource("OfflineDevices", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>线上设备</summary>
        public static string OnlineDevices {
               get {
                   return (string) resourceProvider.GetResource("OnlineDevices", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>待审核设备</summary>
        public static string AuditingDevices {
               get {
                   return (string) resourceProvider.GetResource("AuditingDevices", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>线上设备一览表</summary>
        public static string OnlineDevicesChart {
               get {
                   return (string) resourceProvider.GetResource("OnlineDevicesChart", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>线下设备一览表</summary>
        public static string OfflineDevicesChart {
               get {
                   return (string) resourceProvider.GetResource("OfflineDevicesChart", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>待审核设备一览表</summary>
        public static string AuditingDevicesChart {
               get {
                   return (string) resourceProvider.GetResource("AuditingDevicesChart", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>一键取消申请</summary>
        public static string OneKeyCancel {
               get {
                   return (string) resourceProvider.GetResource("OneKeyCancel", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备一键上线</summary>
        public static string OneKeyOnline {
               get {
                   return (string) resourceProvider.GetResource("OneKeyOnline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备一键下线</summary>
        public static string OneKeyOffline {
               get {
                   return (string) resourceProvider.GetResource("OneKeyOffline", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>取消设备申请</summary>
        public static string CancelAuditing {
               get {
                   return (string) resourceProvider.GetResource("CancelAuditing", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>目标</summary>
        public static string Target {
               get {
                   return (string) resourceProvider.GetResource("Target", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备图片</summary>
        public static string DeviceImage {
               get {
                   return (string) resourceProvider.GetResource("DeviceImage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>设备一览表</summary>
        public static string DeviceList {
               get {
                   return (string) resourceProvider.GetResource("DeviceList", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>添加用户</summary>
        public static string CreateUser {
               get {
                   return (string) resourceProvider.GetResource("CreateUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>头像</summary>
        public static string Head {
               get {
                   return (string) resourceProvider.GetResource("Head", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>用户名</summary>
        public static string UserName {
               get {
                   return (string) resourceProvider.GetResource("UserName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>公司名</summary>
        public static string CompanyName {
               get {
                   return (string) resourceProvider.GetResource("CompanyName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>电话</summary>
        public static string PhoneNumber {
               get {
                   return (string) resourceProvider.GetResource("PhoneNumber", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>邮箱</summary>
        public static string Email {
               get {
                   return (string) resourceProvider.GetResource("Email", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>角色</summary>
        public static string RoleName {
               get {
                   return (string) resourceProvider.GetResource("RoleName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>要删掉这个用户吗?</summary>
        public static string ComfirmDeleteUser {
               get {
                   return (string) resourceProvider.GetResource("ComfirmDeleteUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>编辑用户</summary>
        public static string EditUser {
               get {
                   return (string) resourceProvider.GetResource("EditUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>密码重置</summary>
        public static string ResetPassword {
               get {
                   return (string) resourceProvider.GetResource("ResetPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>密码重置成功</summary>
        public static string ResetPasswordSuccess {
               get {
                   return (string) resourceProvider.GetResource("ResetPasswordSuccess", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>锁定</summary>
        public static string LockUser {
               get {
                   return (string) resourceProvider.GetResource("LockUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>解锁</summary>
        public static string UnLockUser {
               get {
                   return (string) resourceProvider.GetResource("UnLockUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>个人信息</summary>
        public static string UserInfo {
               get {
                   return (string) resourceProvider.GetResource("UserInfo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>基本信息</summary>
        public static string BasicInfo {
               get {
                   return (string) resourceProvider.GetResource("BasicInfo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>密码设置</summary>
        public static string PasswordSetting {
               get {
                   return (string) resourceProvider.GetResource("PasswordSetting", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>最近记录</summary>
        public static string RecentActivity {
               get {
                   return (string) resourceProvider.GetResource("RecentActivity", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>创思官网</summary>
        public static string TroncellOfficalSite {
               get {
                   return (string) resourceProvider.GetResource("TroncellOfficalSite", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>O2O首页</summary>
        public static string o2opage {
               get {
                   return (string) resourceProvider.GetResource("o2opage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>售后服务</summary>
        public static string AfterService {
               get {
                   return (string) resourceProvider.GetResource("AfterService", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>管理后台登陆</summary>
        public static string ManagerLogin {
               get {
                   return (string) resourceProvider.GetResource("ManagerLogin", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>登录</summary>
        public static string Login {
               get {
                   return (string) resourceProvider.GetResource("Login", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>可以使用以下方式登录</summary>
        public static string CanUserBeblowLoginMethod {
               get {
                   return (string) resourceProvider.GetResource("CanUserBeblowLoginMethod", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>忘记密码？</summary>
        public static string ForgotPasswordQuestion {
               get {
                   return (string) resourceProvider.GetResource("ForgotPasswordQuestion", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>找回密码</summary>
        public static string GetBackPassword {
               get {
                   return (string) resourceProvider.GetResource("GetBackPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>发送邮件</summary>
        public static string SendEmail {
               get {
                   return (string) resourceProvider.GetResource("SendEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>发送短信</summary>
        public static string SendSms {
               get {
                   return (string) resourceProvider.GetResource("SendSms", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>重新登陆</summary>
        public static string Relogin {
               get {
                   return (string) resourceProvider.GetResource("Relogin", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>公司介绍</summary>
        public static string CompanyIntroduction {
               get {
                   return (string) resourceProvider.GetResource("CompanyIntroduction", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>社交媒体</summary>
        public static string SocialMedia {
               get {
                   return (string) resourceProvider.GetResource("SocialMedia", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>合作伙伴</summary>
        public static string Partnership {
               get {
                   return (string) resourceProvider.GetResource("Partnership", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>合作伙伴</summary>
        public static string ContactUs {
               get {
                   return (string) resourceProvider.GetResource("ContactUs", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>无锡市新区震泽路18号无锡（国家）软件园水瓶座508室</summary>
        public static string CompanyAddress {
               get {
                   return (string) resourceProvider.GetResource("CompanyAddress", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>选择楼宇</summary>
        public static string SelectBuilding {
               get {
                   return (string) resourceProvider.GetResource("SelectBuilding", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>选择楼层</summary>
        public static string SelectFloor {
               get {
                   return (string) resourceProvider.GetResource("SelectFloor", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        /// <summary>选择房间</summary>
        public static string SelectRoom {
               get {
                   return (string) resourceProvider.GetResource("SelectRoom", CultureInfo.CurrentUICulture.Name);
               }
            }
    }
}
