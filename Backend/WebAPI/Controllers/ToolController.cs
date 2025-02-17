using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using SqlSugar;
using System.Reflection;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        public ISqlSugarClient _db;
        public ToolController(ISqlSugarClient db)
        {
            _db = db;
        }
        [HttpGet]
        public string InitDateBase()
        {
            string res = "OK";
            //如果不存在则创建数据库
            _db.DbMaintenance.CreateDatabase();
            //创建表
            string nspace = "Model.Entitys";
            //通过反射读取我们想要的类
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            //初始化管理员和菜单
            Users user = new Users()
            {
                Name = "admin",
                NickName = "Super Admin",
                Password = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "Default",
                CreateDate = DateTime.Now,
                CreateUserId = 0,
                IsDeleted = 0
            };
            long userId = _db.Insertable(user).ExecuteReturnBigIdentity();
            // 初始化菜单项
            List<Menu> menus = new List<Menu>()
            {
                new Menu()
                {
                    Name = "Manage Menus",
                    Index = "menumanager",
                    FilePath = "../view/admin/menu/MenuManager",
                    ParentId = 0,
                    Order = 0,
                    IsEnable = true,
                    Description = "Default",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId,
                    IsDeleted = 0
                },
                new Menu()
                {
                    Name = "Manage Persons",
                    Index = "personmanager",
                    FilePath = "../view/admin/person/PersonManager",
                    ParentId = 0,
                    Order = 1,
                    IsEnable = true,
                    Description = "Default",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId,
                    IsDeleted = 0
                },
                new Menu()
                {
                    Name = "Manage Roles",
                    Index = "rolemanager",
                    FilePath = "../view/admin/role/RoleManager",
                    ParentId = 0,
                    Order = 2,
                    IsEnable = true,
                    Description = "Default",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId,
                    IsDeleted = 0
                }
            };

            _db.Insertable(menus).ExecuteCommand();
            return res;
        }

        [HttpGet]
        public string Test()
        {
            return "OK";
        }
    }
}
