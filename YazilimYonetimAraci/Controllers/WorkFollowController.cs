using Rotativa;
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
    public class WorkFollowController : Controller
    {
        YazilimYonetimAraciEntities workFollowEdmx = new YazilimYonetimAraciEntities();
        // İŞ DETAYLARI LİSTELEME
        public ActionResult Index(int ProcessID,int ProjectID)
        {
            tblProjects tblproject = workFollowEdmx.tblProjects.Where(P => P.ProjectID == ProjectID).FirstOrDefault();
            tblProcess tblprocess= workFollowEdmx.tblProcess.Where(P => P.ProcessID == ProcessID).FirstOrDefault();
            if (tblproject == null || tblprocess==null)
            {
                ViewBagSet("Projeler", "Projeler", "Düzenle");
                return RedirectToAction("Index");
            }
            Session["ProjectID"] = ProjectID;
            Session["ProcessID"] = ProcessID;
            ViewBag.ProjectID = ProjectID;
            var workFollow = (from W in workFollowEdmx.tblWorkFollow
                              join D in workFollowEdmx.tblWorkFollowDetails on W.WorkFollowDetailsID equals D.WorkFollowDetailsID
                              where W.ProcessID == ProcessID
                              select new
                              {
                                  ProcessID=W.ProcessID,
                                  StartDate=W.StartDate,
                                  FinishDate=W.FinishDate,
                                  WorkFollowDetailsID=W.WorkFollowDetailsID,
                                  UserID=W.UserID,
                                  CompleteRate=W.CompleteRate,
                                  Name=D.WorkFollowName
                              });
            List<ModelWorkFollow> workFollows = new List<ModelWorkFollow>();
            foreach (var isDetay in workFollow)
            {
                ModelWorkFollow modelWork = new ModelWorkFollow();
                modelWork.ProcessID = isDetay.ProcessID;
                modelWork.StartDate = isDetay.StartDate;
                modelWork.FinishDate = isDetay.FinishDate;
                modelWork.WorkFollowDetailsID = isDetay.WorkFollowDetailsID;
                modelWork.UserID = isDetay.UserID;
                modelWork.CompleteRate = isDetay.CompleteRate;
                modelWork.WorkName = isDetay.Name;
                workFollows.Add(modelWork);

            }
            ViewBag.ProcessID = ProcessID;
            ViewBagSet("Projeler", "İş Detayı", "Listele");
            return View(workFollows);
        }

        private void ViewBagSession()
        {
            int ProjectID = Convert.ToInt32(Session["ProjectID"]);
            ViewBag.ProjectID = ProjectID;
            int ProcessID = Convert.ToInt32(Session["ProcessID"]);
            ViewBag.ProcessID = ProcessID;
        }
        //İŞ DETAYI OLUŞTURMA
        public ActionResult Create()
        {

            ViewBagSession();
            ViewBagSet("Projeler", "İş Detayı", "Ekle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelListWorkFollow modelList)
        {
            ViewBagSession();
            bool resultAdd = true;
            bool resultUser = false;
            if (modelList.AnalizWorkFollow.UserID != 0)
            {
                resultUser = true;
                modelList.AnalizWorkFollow.WorkFollowDetailsID = 1;
                resultAdd = WorkFollowAdd(modelList.AnalizWorkFollow);
            }

            if (modelList.TableWorkFollow.UserID != 0)
            {
                resultUser = true;
                modelList.TableWorkFollow.WorkFollowDetailsID = 2;
                resultAdd = WorkFollowAdd(modelList.TableWorkFollow);
            }
            if (modelList.ProcedureWorkFollow.UserID != 0)
            {
                resultUser = true;
                modelList.ProcedureWorkFollow.WorkFollowDetailsID = 3;
                resultAdd = WorkFollowAdd(modelList.ProcedureWorkFollow);
            }
            if (modelList.DllListWorkFollow.UserID != 0)
            {
                resultUser = true;
                modelList.DllListWorkFollow.WorkFollowDetailsID = 4;
                resultAdd = WorkFollowAdd(modelList.DllListWorkFollow);
            }
            if (modelList.FrontWorkFollow.UserID != 0)
            {
                resultUser = true;
                modelList.FrontWorkFollow.WorkFollowDetailsID = 6;
                resultAdd = WorkFollowAdd(modelList.FrontWorkFollow);
            }
            if (modelList.TestWorkFollow.UserID != 0)
            {
                resultUser = true;
                modelList.TestWorkFollow.WorkFollowDetailsID = 7;
                resultAdd = WorkFollowAdd(modelList.TestWorkFollow);
            }

            if (resultAdd == true && resultUser == true)
            {
                workFollowEdmx.SaveChanges();
                return RedirectToAction("Index", "WorkFollow",new {ProcessID=Convert.ToInt32(Session["ProcessID"].ToString()),ProjectID=Convert.ToInt32(Session["ProjectID"].ToString())});
            }
            else
            {
                ViewBagSet("Projeler", "İş Detayı", "Ekle");
                ViewBag.Hata = "Lütfen Bilgileri Kontrol Ediniz";
                return View();
            }
        }
        //Herbir iş için veritabanına ekleme.
        private bool WorkFollowAdd(ModelWorkFollow modelWorkFollow)
        {
            try
            {   //Boşluk Kontrol
                //if (DateAndCompleteRateControl(modelWorkFollow.StartDate.Value,modelWorkFollow.FinishDate.Value,modelWorkFollow.CompleteRate.Value) == false)
                //    return false;
                //Tarih Kontrolleri
                if (modelWorkFollow.StartDate == null || modelWorkFollow.FinishDate == null)
                {
                    ViewBag.Tarih = "TARİHLERİ KONTROL EDİNİZ.";
                    return false;
                }

                if (DateControl(modelWorkFollow.StartDate.Value, modelWorkFollow.FinishDate.Value) != true)
                return false;

                if (modelWorkFollow.UserID != 0 )
            {
                tblWorkFollow tblworkFollow = new tblWorkFollow();
                tblworkFollow.UserID = modelWorkFollow.UserID;
                    tblworkFollow.ProcessID = Convert.ToInt32(Session["ProcessID"].ToString());
                tblworkFollow.StartDate = modelWorkFollow.StartDate;
                tblworkFollow.FinishDate = modelWorkFollow.FinishDate;
                tblworkFollow.CompleteRate = modelWorkFollow.CompleteRate;
                    tblworkFollow.WorkFollowDetailsID = modelWorkFollow.WorkFollowDetailsID;
                workFollowEdmx.tblWorkFollow.Add(tblworkFollow);
            }
            else
            {

            }
          }
            catch (Exception ex)
            {
                throw ex;
                ////Loglama İşlemleri
                //ModelUserLogDetails log = new ModelUserLogDetails();
                //return RedirectToAction("Index", "Error", log);                
            }
            
               
            return true;

        }

        //Navigation
        private void ViewBagSet(string Active, string Menu, string Islem)
        {

            ViewBag.Active = Active;
            ViewBag.Menu = Menu;
            ViewBag.Islem = Islem;
        }

        //Tarih ve Tamamlanma Oranı
        private bool DateAndCompleteRateControl(DateTime StartDate,DateTime FinishDate,int CompleteRate)
        {
            try
            {
                if (StartDate == null ||FinishDate==null)
                {
                    ViewBag.Hata = "TARİHLERİ KONTROL EDİNİZ.";
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }
        //Tarih Kontrolleri
        private bool DateControl(DateTime StartDate, DateTime FinishDate)
        {

            try
            {
                ViewBagSet("Projeler", "İş Akışı", "Ekle");
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
               
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        //public ActionResult PDF(int processID)
        //{
        //    try
        //    {
        //        //var reportModel=
        //    }
        //}
        //public ActionResult ExportPDF(int processID)
        //{
        //    try
        //    {
        //        return new ActionAsPdf("Pdf", new System.Web.Routing.RouteValueDictionary { processID = processID })
        //        {
        //            FileName = Server.MapPath("~/Content/Test.pdf")
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Errors = "Bir hata meydana geldi";
        //        return View();
        //    }
        //}
    }
}