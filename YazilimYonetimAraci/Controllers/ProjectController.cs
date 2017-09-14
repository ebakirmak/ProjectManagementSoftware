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
    public class ProjectController : Controller
    {

        //Veritabanı nesnemiz
        YazilimYonetimAraciEntities projectEdmx = new YazilimYonetimAraciEntities();

        // Tüm Projeleri Listeleme
        public ActionResult Index()
        {
            List<ModelProject> myProjects = new List<ModelProject>();
            int UserID = 0;
            int RolID = 0;
            try
            {
                UserID = Convert.ToInt32(Session["UserID"].ToString());
                RolID = Convert.ToInt32(Session["RoleID"].ToString());
                if (RolID == 0)
                {
                    ModelUserLogDetails log = new ModelUserLogDetails();
                    //Loglama İşlemleri
                    return RedirectToAction("Index", "Error", log);
                }
                else if (RolID == 1)
                {
                    //Müşteri
                    
                    var varMyProjects = (from U in projectEdmx.tblUsers
                                         join P in projectEdmx.tblProjects on U.UsersID equals P.ProjectOwnerID
                                         where P.ProjectOwnerID == UserID
                                         select new
                                         {
                                             ProjectID = P.ProjectID,
                                             ProjectName = P.ProjectName,
                                             StartDate = P.StartDate,
                                             FinishDate = P.FinishDate,
                                             isActive = P.isActive,
                                             ProjectOwnerID = P.ProjectOwnerID,
                                             ProjectManagerID = P.ProjectManagerID,
                                             Budget = P.Budget
                                         });
                    if (varMyProjects == null)
                    {
                        ModelUserLogDetails log = new ModelUserLogDetails();
                        //Loglama İşlemleri
                        return RedirectToAction("Index", "Error", log);
                    }
                    try
                    {
                        foreach (var proje in varMyProjects)
                        {
                            ModelProject myProject = new ModelProject();
                            myProject.ProjectID = proje.ProjectID;
                            myProject.ProjectName = proje.ProjectName;
                            myProject.ProjectManagerID = proje.ProjectManagerID;
                            myProject.StartDate = proje.StartDate;
                            myProject.FinishDate = proje.FinishDate;
                            myProject.Budget = proje.Budget;
                            myProject.isActive = proje.isActive;
                            if (proje.isActive==true)
                            myProject.isActiveString = "Devam Ediyor";
                            else
                             myProject.isActiveString = "Sonlandı.";
                            myProject.ProjectOwnerID = proje.ProjectOwnerID;
                            myProjects.Add(myProject);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                        ////Loglama İşlemleri
                        //ModelUserLogDetails log = new ModelUserLogDetails();
                        //return RedirectToAction("Index", "Error", log);
                    }

                }
                else if (RolID == 2)
                {
                    //Proje Yöneticisi
                    var varMyProjects = (from U in projectEdmx.tblUsers
                                         join P in projectEdmx.tblProjects on U.UsersID equals P.ProjectOwnerID
                                         where P.ProjectManagerID == UserID
                                         select new
                                         {
                                             ProjectID = P.ProjectID,
                                             ProjectName = P.ProjectName,
                                             StartDate = P.StartDate,
                                             FinishDate = P.FinishDate,
                                             isActive = P.isActive,
                                             ProjectOwnerID = P.ProjectOwnerID,
                                             ProjectManagerID = P.ProjectManagerID,
                                             Budget = P.Budget
                                         });
                    if (varMyProjects == null)
                    {
                        ModelUserLogDetails log = new ModelUserLogDetails();
                        //Loglama İşlemleri
                        return RedirectToAction("Index", "Error", log);
                    }
                    try
                    {
                        foreach (var proje in varMyProjects)
                        {
                            ModelProject myProject = new ModelProject();
                            myProject.ProjectID = proje.ProjectID;
                            myProject.ProjectName = proje.ProjectName;
                            myProject.ProjectManagerID = proje.ProjectManagerID;
                            myProject.StartDate = proje.StartDate;
                            myProject.FinishDate = proje.FinishDate;
                            myProject.Budget = proje.Budget;
                            myProject.isActive = proje.isActive;
                            if (proje.isActive == true)
                                myProject.isActiveString = "Devam Ediyor";
                            else
                                myProject.isActiveString = "Sonlandı.";
                            myProject.ProjectOwnerID = proje.ProjectOwnerID;
                            myProjects.Add(myProject);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                        ////Loglama İşlemleri
                        //ModelUserLogDetails log = new ModelUserLogDetails();
                        //return RedirectToAction("Index", "Error", log);
                    }
                }
                else
                {
                    var varMyProjects = (from UP in projectEdmx.tblUserProcess
                                         join PSS in projectEdmx.tblProcess on UP.ProcessID equals PSS.ProcessID
                                         join P in projectEdmx.tblProjects on PSS.ProjectID equals P.ProjectID
                                         where UP.UsersID == UserID
                                         select new
                                         {
                                             ProjectID = P.ProjectID,
                                             ProjectName = P.ProjectName,
                                             StartDate = P.StartDate,
                                             FinishDate = P.FinishDate,
                                             isActive = P.isActive,
                                             ProjectOwnerID = P.ProjectOwnerID,
                                             ProjectManagerID = P.ProjectManagerID,
                                             Budget = P.Budget
                                         });
                    if (varMyProjects == null)
                    {
                        ModelUserLogDetails log = new ModelUserLogDetails();
                        //Loglama İşlemleri
                        return RedirectToAction("Index", "Error", log);
                    }
                    try
                    {
                        int durumID = 0;
                        foreach (var proje in varMyProjects)
                        {
                            if(durumID!=proje.ProjectID)
                            {
                                ModelProject myProject = new ModelProject();
                                myProject.ProjectID = proje.ProjectID;
                                myProject.ProjectName = proje.ProjectName;
                                myProject.ProjectManagerID = proje.ProjectManagerID;
                                myProject.StartDate = proje.StartDate;
                                myProject.FinishDate = proje.FinishDate;
                                myProject.Budget = proje.Budget;
                                myProject.isActive = proje.isActive;
                                if (proje.isActive == true)
                                    myProject.isActiveString = "Devam Ediyor";
                                else
                                    myProject.isActiveString = "Sonlandı.";
                                myProject.ProjectOwnerID = proje.ProjectOwnerID;
                                myProjects.Add(myProject);
                            }                        
                            durumID = proje.ProjectID;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        ////Loglama İşlemleri
                        //ModelUserLogDetails log = new ModelUserLogDetails();
                        //return RedirectToAction("Index", "Error", log);
                    }


                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "UserRoles");
                //LOGLAMA İŞLEMLERİ
            }
            ViewBagSet("Projeler", "Projeler", "Tüm Projeler");
            return View(myProjects);
        }      

        //Yeni Proje Ekleme - Sadece Müşteriler Ekliyor
        public ActionResult Create()
        {
            ViewBagSet("Projeler", "Projeler", "Ekle");
            return View();
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelProject modelProject)
        {
            if (modelProject == null)
                return RedirectToAction("Index", "Error");

            //Tarih Kontrolleri
            if (DateControl(modelProject.StartDate.Value, modelProject.FinishDate.Value) != true)
                return View();

            tblProjects project = new tblProjects();
            try
            {
                project.ProjectName = modelProject.ProjectName;
                project.StartDate = modelProject.StartDate;
                project.FinishDate = modelProject.FinishDate;
                project.ProjectOwnerID = Convert.ToInt32(Session["UserID"].ToString());
                project.ProjectManagerID = Convert.ToInt32(1);
                project.isActive = true;
                project.Budget = modelProject.Budget;
                projectEdmx.tblProjects.Add(project);
            }
            catch (Exception ex)
            {
                throw ex;
                ////Loglama İşlemleri
                //ModelUserLogDetails log = new ModelUserLogDetails();
                //return RedirectToAction("Index", "Error", log);                
            }
            finally
            {
                projectEdmx.SaveChanges();
            }

            ViewBagSet("Projeler", "Projeler", "Tüm Projeler");
            return RedirectToAction("Index");
        }
        
        //Proje Bilgi Güncelleme
        public ActionResult Edit(int ProjectID)
        {
            tblProjects tblproject = projectEdmx.tblProjects.Where(P => P.ProjectID == ProjectID).FirstOrDefault();
            if (tblproject == null)
            {
                ViewBagSet("Projeler", "Projeler", "Düzenle");
                return RedirectToAction("Index");
            }
            if (tblproject.isActive == false)
            {
                return RedirectToAction("Index");
            }
          
            ModelProject modelproject = new ModelProject();
            modelproject.ProjectID = tblproject.ProjectID;
            modelproject.ProjectName = tblproject.ProjectName;
            modelproject.FinishDate = tblproject.FinishDate;
            modelproject.StartDate = tblproject.StartDate;
            modelproject.ProjectOwnerID = tblproject.ProjectOwnerID;
            modelproject.ProjectManagerID = tblproject.ProjectManagerID;
            modelproject.Budget = tblproject.Budget;
            modelproject.isActive = tblproject.isActive;
            ViewBagSet("Projeler", "Projeler", "Düzenle");
            return View(modelproject);

        }     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelProject modelProject)
        {try
            {
                tblProjects tblProject = new tblProjects();
                tblProject = projectEdmx.tblProjects.Where(P => P.ProjectID == modelProject.ProjectID).FirstOrDefault();
                if (tblProject == null)
                    return RedirectToAction("Index", "Error");

                //Tarih Kontrolleri
                if (DateControl(modelProject.StartDate.Value, modelProject.FinishDate.Value) != true)
                    return View();

                tblProject.ProjectName = modelProject.ProjectName;
                tblProject.FinishDate = modelProject.FinishDate;
                tblProject.StartDate = modelProject.StartDate;
                tblProject.ProjectManagerID = modelProject.ProjectManagerID;
                tblProject.Budget = modelProject.Budget;              
                tblProject.isActive = modelProject.isActive;
                tblProject.ProjectID = modelProject.ProjectID;
            }
            catch (Exception ex)
            {
                throw ex;
                ////Loglama İşlemleri
                //ModelUserLogDetails log = new ModelUserLogDetails();
                //return RedirectToAction("Index", "Error", log);     
                
            }
            finally
            {
                projectEdmx.SaveChanges();
            }
            ViewBagSet("Projeler", "Projeler", "Düzenle");
            return RedirectToAction("Index");
        }

        //Proje Sonlandırma
        public ActionResult Delete(int ProjectID)
        {
            tblProjects tblproject = projectEdmx.tblProjects.Where(P => P.ProjectID == ProjectID).FirstOrDefault();
            if (tblproject == null)
            {
                ViewBagSet("Projeler", "Projeler", "Düzenle");
                return RedirectToAction("Index");
            }
            if (tblproject.isActive == false)
            {
                return RedirectToAction("Index");
            }
            ModelProject modelproject = new ModelProject();
            modelproject.ProjectID = tblproject.ProjectID;
            modelproject.ProjectName = tblproject.ProjectName;
            modelproject.FinishDate = tblproject.FinishDate;
            modelproject.StartDate = tblproject.StartDate;
            modelproject.ProjectOwnerID = tblproject.ProjectOwnerID;
            modelproject.ProjectManagerID = tblproject.ProjectManagerID;
            modelproject.Budget = tblproject.Budget;
            modelproject.isActive = tblproject.isActive;
            ViewBagSet("Projeler", "Projeler", "Sil");
            return View(modelproject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ModelProject modelProject)
        {
            tblProjects tblProject = new tblProjects();
            tblProject = projectEdmx.tblProjects.Where(P => P.ProjectID == modelProject.ProjectID).FirstOrDefault();
            if (tblProject == null)
                return RedirectToAction("Index", "Error");          

            try
            {
              
                tblProject.isActive = false;
             
            }
            catch (Exception ex)
            {
                throw ex;
                ////Loglama İşlemleri
                //ModelUserLogDetails log = new ModelUserLogDetails();
                //return RedirectToAction("Index", "Error", log);     

            }
            finally
            {
                projectEdmx.SaveChanges();
            }
            ViewBagSet("Projeler", "Projeler", "Düzenle");
            return RedirectToAction("Index");
        }
        //Proje Yöneticisi Rolü Olan Kullanıcıları Seçme
        public JsonResult ProjectManagers()
        {
                        var projectManagers = (from UR in projectEdmx.tblUserRoles
                                   join U in projectEdmx.tblUsers on UR.UsersID equals U.UsersID
                                   where UR.RoleNameID == 2
                                   select new
                                   {
                                       ID = U.UsersID,
                                       Name = U.UserName + " "+ U.UserSurname
                                   });
            return Json(projectManagers);
        }

        //Navigation
        private void ViewBagSet(string Active, string Menu, string Islem)
        {

            ViewBag.Active = Active;
            ViewBag.Menu = Menu;
            ViewBag.Islem = Islem;
        }

        //Tarih Kontrolleri
        private bool DateControl(DateTime StartDate, DateTime FinishDate)
        {


            ViewBagSet("Projeler", "Projeler", "Ekle");
            if (StartDate == null || FinishDate == null)
            {
                ViewBag.TarihKarsilastirma = "Başlangıç ve Bitiş Tarihlerini Giriniz";

                return false;
            }
            else if (FinishDate < StartDate)
            {
                ViewBag.TarihKarsilastirma = "Bitiş tarihi başlangıç tarihinden küçük olamaz.";
                return false;
            }
            else if (FinishDate < DateTime.Today)
            {
                ViewBag.TarihKarsilastirma = "Bitiş tarihi bugünden önce olamaz.";
                return false;
            }
            else if (StartDate.Year > 2099 || FinishDate.Year > 2099)
            {
                ViewBag.TarihKarsilastirma = "Tarih yılları 2099'dan büyük olamaz";
                return false;
            }
            else if (StartDate.Year < 1900)
            {
                ViewBag.TarihKarsilastirma = "Tarih başlangıcı 1900'dan küçük olamaz";
                return false;
            }
            return true;
        }
    }
}