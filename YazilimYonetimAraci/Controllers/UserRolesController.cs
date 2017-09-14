using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazilimYonetimAraci.Helper;
using YazilimYonetimAraci.Models;

namespace YazilimYonetimAraci.Controllers
{
    [CustomFilter]
    public class UserRolesController : Controller
    {
        YazilimYonetimAraciEntities userRolesEdmx = new YazilimYonetimAraciEntities();
        // GET: UserRoles
  
        public ActionResult Index()
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
         var tbluserRoles = userRolesEdmx.tblUserRoles.Where(UR => UR.UsersID == UserID).ToList();
            List<ModelUserRolesAndRoleNames> ListUserRolesAndRoleNames = new List<ModelUserRolesAndRoleNames>();
           
            foreach (var rol in tbluserRoles)
            {
                ModelUserRolesAndRoleNames modelUserRoleAndRoleNames = new ModelUserRolesAndRoleNames();
                ModelUserRoles modelUserRole = new ModelUserRoles();
                modelUserRole.UserRolesID = rol.UserRolesID;
                modelUserRole.UserID = rol.UsersID;
                modelUserRole.RoleNameID = rol.RoleNameID;
                modelUserRoleAndRoleNames.UserRoles = modelUserRole;

                var tblRoleNames = userRolesEdmx.tblRoleNames.Where(RN => RN.RoleNameID == modelUserRole.RoleNameID).FirstOrDefault();
                if (tblRoleNames == null)
                    return View("Error");
                ModelRoleNames modelRoleNames = new ModelRoleNames();
                modelRoleNames.RoleNameID = tblRoleNames.RoleNameID;
                modelRoleNames.RoleName = tblRoleNames.RoleName;
                modelUserRoleAndRoleNames.RoleNames = modelRoleNames;
                ListUserRolesAndRoleNames.Add(modelUserRoleAndRoleNames);
            }
            ViewBagSet("Rol", "Kullanıcı Rolleri", "Rol Seçme");
            return View(ListUserRolesAndRoleNames);
        }
        public void ViewBagSet(string Active, string Menu, string Islem)
        {

            ViewBag.Active = Active;
            ViewBag.Menu = Menu;
            ViewBag.Islem = Islem;
        }
        public ActionResult RoleSet(int RoleNameID)
        {
            if (RoleNameID == 0)
                return RedirectToAction("Index","Error");

            Session["RoleID"] = RoleNameID;

            return RedirectToAction("Index", "Project");
        }

        public JsonResult RoleGet()
        {
            int RoleID = 0;

            try
            {
                RoleID = Convert.ToInt32(Session["RoleID"].ToString());
            }
            catch (Exception ex)
            {

                //throw ex;
            }
            string RolName = (from RN in userRolesEdmx.tblRoleNames
                              where RN.RoleNameID == RoleID
                              select RN.RoleName).FirstOrDefault();
            if(RolName!=null)
                return Json(RolName);
            else
            {
                return Json("HATA");
            }
        }
     
    }
}