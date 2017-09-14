using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.BL
{
    public class LoginTest
    {
        public bool Login(string username, string password)
        {
            YazilimYonetimAraciEntities ent = new YazilimYonetimAraciEntities();

            var user = ent.tblUsers.FirstOrDefault(u => u.UserNickname == username && u.UserPassword == password);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public bool EditProject(string ProjectName,DateTime FinishDate, DateTime StartDate,int ProjectManagerID,decimal Budget,bool isActive, int ProjectID)
        {
            YazilimYonetimAraciEntities ent = new YazilimYonetimAraciEntities();
            tblProjects tblProject = new tblProjects();
            tblProject = ent.tblProjects.Where(P => P.ProjectID == ProjectID).FirstOrDefault();

            if (tblProject == null)
                return false;
       
            tblProject.ProjectName = ProjectName;
            tblProject.FinishDate = FinishDate;
            tblProject.StartDate = StartDate;
            tblProject.ProjectManagerID = ProjectManagerID;
            tblProject.Budget = Budget;
            tblProject.isActive = isActive;
            tblProject.ProjectID = ProjectID;
            ent.SaveChanges();
            return true;
        }

     
    }
}