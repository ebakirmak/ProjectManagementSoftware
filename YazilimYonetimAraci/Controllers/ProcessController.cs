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
    public class ProcessController : Controller
    {
        YazilimYonetimAraciEntities processEdmx = new YazilimYonetimAraciEntities();
        //Tüm Prosesleri Listeleme
        public ActionResult Index(int ProjectID)
        {
            List<ModelProcess> myAllProcess = new List<ModelProcess>();
            tblProjects tblproject = processEdmx.tblProjects.Where(P => P.ProjectID == ProjectID).FirstOrDefault();
            if (tblproject == null)
            {
                return RedirectToAction("Index", "Project");
                
            }
            List< ModelProcess> varMainProcess =  ProcessGet(ProjectID, 0);
            if (varMainProcess == null)
            {
                ModelUserLogDetails log = new ModelUserLogDetails();
                //Loglama İşlemleri
                return RedirectToAction("Index", "Error", log);
            }
            try
            {
                for (int i = 0; i < varMainProcess.Count; i++)
                {
                    myAllProcess.Add(varMainProcess[i]);
                    List<ModelProcess> varProcess = ProcessGet(ProjectID, varMainProcess[i].ProcessID);
                    for (int j = 0; j < varProcess.Count; j++)
                    {
                        myAllProcess.Add(varProcess[j]);
                        List<ModelProcess> varSubProcess = ProcessGet(ProjectID, varProcess[j].ProcessID);
                        for (int k = 0; k < varSubProcess.Count; k++)
                        {
                            myAllProcess.Add(varSubProcess[k]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //ModelUserLogDetails log = new ModelUserLogDetails();
                ////Loglama İşlemleri
                //return RedirectToAction("Index", "Error", log);
            }
            ViewBag.ProjectID = ProjectID;
            ViewBagSet("Projeler", "Süreçler", "Listele");
            return View(myAllProcess);
        }

        //Tüm Processleri Getirmek
        private List<ModelProcess> ProcessGet(int ProjectID, int ParentID)
        {
            List<ModelProcess> listProcess = new List<ModelProcess>();        
          List<tblProcess> allProcess = processEdmx.tblProcess.Where(P => P.ProjectID == ProjectID && P.ParentID == ParentID).ToList();

            foreach (var process in allProcess)
            {
                ModelProcess modelProcess = new ModelProcess();
                modelProcess.ProcessID = process.ProcessID;
                modelProcess.ProcessID = process.ProcessID;
                modelProcess.ProcessName = process.ProcessName;
                modelProcess.ProjectID = process.ProjectID;
                modelProcess.StartDate = process.StartDate;
                modelProcess.FinishDate = process.FinishDate;
                modelProcess.Duration = process.Duration;
                modelProcess.CompleteRate = process.CompleteRate;
                modelProcess.Priority = process.Priority;
                if (process.Priority == 1)
                    modelProcess.PriorityString = "Acil";
                else if (process.Priority == 2)
                    modelProcess.PriorityString = "Önemli";
                else if (process.Priority == 3)
                    modelProcess.PriorityString = "Normal";
                else if (process.Priority == 4)
                    modelProcess.PriorityString = "Esnek";
                    modelProcess.ParentID = process.ParentID;
                modelProcess.Notes = process.Notes;
                modelProcess.Descriptions = process.Descriptions;
                modelProcess.ProcessManagerID = process.ProcessManagerID;
                listProcess.Add(modelProcess);
            }
            return listProcess;
        }

        //Process Oluşturmak       
        public ActionResult Create(int ProjectID)
        {
            tblProjects tblproject = processEdmx.tblProjects.Where(P => P.ProjectID == ProjectID).FirstOrDefault();
            if (tblproject == null)
            {
                return RedirectToAction("Index", "Project");

            }
            ModelProcess mp = new ModelProcess();
            if (ProjectID != 0)
            {
               
                mp.ProjectID = ProjectID;
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
            ViewBagSet("", "Süreçler", "Ekle");
            return View(mp);
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelProcess modelProcess)
        {
            if (modelProcess == null)
                return View();
            //Tarih Kontrolleri
            if (DateControl(modelProcess.StartDate.Value, modelProcess.FinishDate.Value) != true)
                return View(modelProcess);
            tblProcess tblprocess = new tblProcess();
            try
            {
                tblprocess.ProcessName = modelProcess.ProcessName;
                tblprocess.ProjectID = modelProcess.ProjectID;
                tblprocess.CompleteRate = 0;
                tblprocess.Descriptions = modelProcess.Descriptions;
                tblprocess.Duration = modelProcess.Duration;
                tblprocess.FinishDate = modelProcess.FinishDate;
                tblprocess.StartDate = modelProcess.StartDate;
                tblprocess.ParentID = modelProcess.ParentID;
                tblprocess.ProcessManagerID = modelProcess.ProcessManagerID;
                tblprocess.Notes = modelProcess.Notes;
                tblprocess.Priority = modelProcess.Priority;
                processEdmx.tblProcess.Add(tblprocess);

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                processEdmx.SaveChanges();
            }
            return RedirectToAction("Index",new { modelProcess.ProjectID });
        }

        //Process Bilgi Güncelleme
        public ActionResult Edit(int ProcessID)
        {
            tblProcess tblprocess = processEdmx.tblProcess.Where(P => P.ProcessID == ProcessID).FirstOrDefault();
            if (tblprocess == null)
            {               
                return RedirectToAction("Index","Project");
            }
           
            ModelProcess modelProcess = new ModelProcess();
            modelProcess.ProjectID = tblprocess.ProjectID;
            modelProcess.ProcessName = tblprocess.ProcessName;
            modelProcess.FinishDate = tblprocess.FinishDate;
            modelProcess.StartDate = tblprocess.StartDate;
            modelProcess.ProcessManagerID = tblprocess.ProcessManagerID;
            modelProcess.ParentID = tblprocess.ParentID;
            modelProcess.Priority = tblprocess.Priority;
            modelProcess.Duration = tblprocess.Duration;
            modelProcess.Descriptions = tblprocess.Descriptions;
            ViewBagSet("", "Süreçler", "Düzenle");
            return View(modelProcess);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelProcess modelProcess)
        {
            try
            {
                tblProcess tblprocess = new tblProcess();
                tblprocess = processEdmx.tblProcess.Where(P => P.ProcessID == modelProcess.ProcessID).FirstOrDefault();
                if (tblprocess == null)
                    return RedirectToAction("Index", "Error");

                //Tarih Kontrolleri
                if (DateControl(modelProcess.StartDate.Value, modelProcess.FinishDate.Value) != true)
                    return View(modelProcess);
                if(modelProcess.ParentID==modelProcess.ProcessID)
                {
                    ViewBag.Parent = "Parent Kısmında Hatalı Giriş";
                    return View(modelProcess);
                }

                tblprocess.ProcessName = modelProcess.ProcessName;
                tblprocess.FinishDate = modelProcess.FinishDate;
                tblprocess.StartDate = modelProcess.StartDate;
                tblprocess.ProcessManagerID = modelProcess.ProcessManagerID;
                tblprocess.ProjectID = modelProcess.ProjectID;
                tblprocess.Priority = modelProcess.Priority;
                tblprocess.Duration = modelProcess.Duration;
                tblprocess.Descriptions = modelProcess.Descriptions;

            }
            catch (Exception ex)
            {
                throw ex;
                ////Loglama İşlemleri
                //ModelUserLogDetails log = new ModelUserLogDetails();
                //return RedirectToAction("Index", "Error", log);     

            }
          
            processEdmx.SaveChanges();            
            ViewBagSet("Projeler", "Süreçler", "Düzenle");
            return RedirectToAction("Index", new { modelProcess.ProjectID });
        }

        //Proje Sonlandırma
        public ActionResult Delete(int ProcessID)
        {
            tblProcess tblprocess = processEdmx.tblProcess.Where(P => P.ProcessID == ProcessID).FirstOrDefault();
            if (tblprocess == null)
            {               
                return RedirectToAction("Index");
            }         
            ModelProcess modelProcess = new ModelProcess();
            modelProcess.ProjectID = tblprocess.ProjectID;
            modelProcess.ProcessName = tblprocess.ProcessName;
            modelProcess.FinishDate = tblprocess.FinishDate;
            modelProcess.StartDate = tblprocess.StartDate;
            modelProcess.ProcessManagerID = tblprocess.ProcessManagerID;
            modelProcess.ParentID = tblprocess.ParentID;
            modelProcess.Priority = tblprocess.Priority;
            modelProcess.Duration = tblprocess.Duration;
            modelProcess.Descriptions = tblprocess.Descriptions;
            ViewBagSet("Projeler", "Süreçler", "Düzenle");
            return View(modelProcess);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ModelProcess modelProcess)
        {
            tblProcess tblProcess = new tblProcess();
            tblProcess = processEdmx.tblProcess.Where(P => P.ProcessID == modelProcess.ProcessID).FirstOrDefault();
            if (tblProcess == null)
                return RedirectToAction("Index", "Error");

            try
            {

                processEdmx.tblProcess.Remove(tblProcess);

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
                processEdmx.SaveChanges();
            }            
            return RedirectToAction("Index",new { modelProcess.ProjectID } );
        }

        //Tüm Ana süreçleri getir.
        public JsonResult MainProcessGet(int ProjectID)
        {
            List<ModelProcess> ListMainProcess = new List<ModelProcess>();
            try
            {
                var varMainProcess = (from P in processEdmx.tblProcess
                                      where P.ParentID == 0 && P.ProjectID== ProjectID
                                      select new
                                      {
                                          MainProcessID = P.ProcessID,
                                          MainProcessName = P.ProcessName
                                      });

                foreach (var mainProcess in varMainProcess)
                {
                    ModelProcess mp = new ModelProcess();
                    mp.ProcessName = mainProcess.MainProcessName;
                    mp.ProcessID = mainProcess.MainProcessID;
                    ListMainProcess.Add(mp);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
          

            return Json(ListMainProcess);
        }

        //Tüm prosesleri getir.
        public JsonResult ProcessGet(int ProjectID)
        {
            List<ModelProcess> ListProcess = new List<ModelProcess>();
            try
            {
                var varMainProcess = (from P in processEdmx.tblProcess
                                      where P.ParentID == 0 && P.ProjectID==ProjectID
                                      select new
                                      {
                                          MainProcessID = P.ProcessID,
                                          MainProcessName = P.ProcessName
                                      });

                foreach (var mainProcess in varMainProcess)
                {
                    var varProcess = (from P in processEdmx.tblProcess
                                          where P.ParentID == mainProcess.MainProcessID && P.ProjectID==ProjectID
                                          select new
                                          {
                                              ProcessID = P.ProcessID,
                                              ProcessName = P.ProcessName
                                          });
                    foreach (var process in varProcess)
                    {
                        ModelProcess mp = new ModelProcess();
                        mp.ProcessName = process.ProcessName;
                        mp.ProcessID = process.ProcessID;
                        ListProcess.Add(mp);
                    
                    }
                 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


  

            return Json(ListProcess);
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