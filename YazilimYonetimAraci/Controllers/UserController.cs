using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazilimYonetimAraci.Models;

namespace YazilimYonetimAraci.Controllers
{
    public class UserController : Controller
    {

        //Veritabanı Nesnemiz
        YazilimYonetimAraciEntities userEdmx = new YazilimYonetimAraciEntities();

        // Kayıt Olma Sayfasını Gösterdi...
        public ActionResult SignUp()
        {
            return View();
        }

        //Kayıt Bilgilerini döndürüyor...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(tblUsers m)
        {
            //Veritabanı Ekle...  
            try
            {

                if (userEdmx.tblUsers.Where(U => U.UserNickname == m.UserNickname.Trim()).FirstOrDefault() == null)
                {
                    tblUsers kayitOlanKullanici = new tblUsers();
                    kayitOlanKullanici.UserName = m.UserName.Trim();
                    kayitOlanKullanici.UserNickname = m.UserNickname.Trim();
                    kayitOlanKullanici.UserPassword = m.UserPassword;
                    kayitOlanKullanici.UserSurname = m.UserSurname.Trim();
                    kayitOlanKullanici.UserCreatedDate = DateTime.Now;
                    kayitOlanKullanici.isActive = true;
                    userEdmx.tblUsers.Add(kayitOlanKullanici);
                }
                else
                {
                    ViewBag.Mesaj = "Böyle bir kullanıcı Mevcuttur.";
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;
                //Loglama İşlemleri
                //ModelUserLogDetails log = new ModelUserLogDetails();
                //return RedirectToAction("Index", "Error", log);
            }
            finally
            {
                userEdmx.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        //Kullanıcı Giriş Sayfası
        public ActionResult Login()
        {
            return View();
        }

        //Kullanıcı Giriş Sağlanıyor...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ModelUser m)
        {
            //Veritabanından kontrol et...
            try
            {
                var usernameList = userEdmx.tblUsers.Select(u => u.UserNickname).ToList();
                var durum = false;
                foreach (var username2 in usernameList)
                {
                    if (username2 == m.UserNickname)
                    {
                        durum = true;
                    }
                }

                if(durum==false)
                {
                    return View();
                }

                tblUsers kullanici = userEdmx.tblUsers.Where(U => U.UserNickname == m.UserNickname.Trim() && U.UserPassword == m.UserPassword).FirstOrDefault();
                if (kullanici != null)
                {

                    Session["UserID"] = kullanici.UsersID;
                    return RedirectToAction("Index", "UserRoles");
                }
                else
                {
                    //Loglama İşlemleri
                    ViewBag.Mesaj = "Hatalı Kullanıcı Adı veya Parola";
                    return View();
                }
            }
            catch (Exception ex)
            {


                throw ex;
                //Loglama İşlemleri
                //ModelUserLogDetails log = new ModelUserLogDetails();
                //return RedirectToAction("Index", "Error", log);
            }



        }


        //Kullanıcı Şifre Değiştir
        public ActionResult ChangePassword()
        {
            return View();
        }

        //Kullanıcı Bilgileri Güncelle
        public ActionResult UpdateProfileInformation()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        //Kullanıcı Bilgilerini Getir.
        public JsonResult GetUserNames()
        {
            tblUsers databaseUser = new tblUsers();
            int UserID = 0;
            try
            {
                UserID = Convert.ToInt32(Session["UserID"].ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }

            databaseUser = userEdmx.tblUsers.Where(U => U.UsersID == UserID).FirstOrDefault();
            if (databaseUser == null)
                return Json(0);

            ModelUser modelUser = new ModelUser();
            modelUser.UserName = databaseUser.UserName;
            modelUser.UserSurname = databaseUser.UserSurname;


            return Json(modelUser);
        }

        public ActionResult LogOut()
        {
            try
            {

                Session["UserID"] = null;
                Session["RoleID"] = null;
            }
            catch (Exception ex)
            {
                //Loglama İşlemleri
                throw ex;
            }

            return RedirectToAction("Login");
        }

        //Sistemdeki herkesi getir
        public JsonResult GetUsers()
        {

            List<ModelUser> modelUsers = new List<ModelUser>();
            try
            {
                int usersID = Convert.ToInt32(Session["UserID"].ToString());
                var users = (from U in userEdmx.tblUsers
                             join UR in userEdmx.tblUserRoles on U.UsersID equals UR.UsersID
                             where U.UsersID != usersID && UR.RoleNameID != 1
                             select new
                             {
                                 UserName = U.UserName,
                                 UserSurname = U.UserSurname,
                                 UserNickName = U.UserNickname,
                                 UserID = U.UsersID

                             }).Distinct().ToList();

                foreach (var user in users)
                {
                    ModelUser modelUser = new ModelUser();
                    modelUser.UserName = user.UserName;
                    modelUser.UserSurname = user.UserSurname;
                    modelUser.UsersID = user.UserID;
                    modelUser.UserNickname = user.UserNickName;
                    modelUsers.Add(modelUser);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(modelUsers);
        }

        public JsonResult GetAnalyzers()
        {
            List<ModelUser> Analyzers = GetAll(5);
            return Json(Analyzers);
        }

        public JsonResult GetDatabaseManagement()
        {
            List<ModelUser> DatabaseManagement = GetAll(6);
            return Json(DatabaseManagement);
        }

        public JsonResult GetProcedure()
        {
            List<ModelUser> Procedure = GetAll(7);
            return Json(Procedure);
        }

        public JsonResult GetDllList()
        {
            List<ModelUser> DllList = GetAll(8);
            return Json(DllList);
        }
        public JsonResult GetFrontEnd()
        {
            List<ModelUser> FrontEnd = GetAll(4);
            return Json(FrontEnd);
        }
        public JsonResult GetTest()
        {
            List<ModelUser> FrontEnd = GetAll(9);
            return Json(FrontEnd);
        }
        public List<ModelUser> GetAll(int RolNamesID)
        {

            List<ModelUser> modelUsers = new List<ModelUser>();
            try
            {
                //int usersID = Convert.ToInt32(Session["UserID"].ToString());
                var users = (from U in userEdmx.tblUsers
                             join UR in userEdmx.tblUserRoles on U.UsersID equals UR.UsersID
                             where UR.RoleNameID == RolNamesID
                             select new
                             {
                                 UserName = U.UserName,
                                 UserSurname = U.UserSurname,
                                 UserNickName = U.UserNickname,
                                 UserID = U.UsersID

                             }).Distinct().ToList();

                foreach (var user in users)
                {
                    ModelUser modelUser = new ModelUser();
                    modelUser.UserName = user.UserName;
                    modelUser.UserSurname = user.UserSurname;
                    modelUser.UsersID = user.UserID;
                    modelUser.UserNickname = user.UserNickName;
                    modelUsers.Add(modelUser);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return modelUsers;
        }

        public bool Kontrol(int UserID){
            if (UserID > 0)
                return true;
            return false;
            }

    }
}