using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelListWorkFollow
    {
        public ModelWorkFollow AnalizWorkFollow { get; set; }

        public ModelWorkFollow TableWorkFollow { get; set; }

        public ModelWorkFollow ProcedureWorkFollow { get; set; }

        public ModelWorkFollow DllListWorkFollow { get; set; }

        public ModelWorkFollow FrontWorkFollow { get; set; }

        public ModelWorkFollow TestWorkFollow { get; set; }

        public ModelListWorkFollow()
        {
            this.AnalizWorkFollow = new ModelWorkFollow();
            //this.AnalizWorkFollow.FinishDate = DateTime.Today;
            //this.AnalizWorkFollow.StartDate = DateTime.Today;
            this.TableWorkFollow = new ModelWorkFollow();
            this.ProcedureWorkFollow = new ModelWorkFollow();
            this.DllListWorkFollow = new ModelWorkFollow();
            this.FrontWorkFollow = new ModelWorkFollow();
            this.TestWorkFollow = new ModelWorkFollow();
        }
    }
}