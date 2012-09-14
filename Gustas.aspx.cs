using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using D3Entities;

public partial class Gustas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using(Diablo3ProfilerEntities db = new Diablo3ProfilerEntities())
        {
            string battleTag = "Gustas#fake";
            CarrerProfile career = db.CarrerProfiles.FirstOrDefault(c => c.BattleTag == battleTag);
            if(career != null)
            {
                //update
            }
            else
            {
                //crate new
                career = new CarrerProfile();
                career.BattleTag = battleTag;
                career.Id = -1;
                career.KillsElites = 6001;
                career.KillsHardcoreMonsters = 69;
                career.KillsMonsters = 9999;
                career.LastUpdated = DateTime.UtcNow;
                career.TimePlayedBarbarian = 0;
                career.TimePlayedDemonHunter = 0;
                career.TimePlayedMonk = 666.66;
                career.TimePlayedWitchDoctor = -1;
                career.TimePlayedWizard = 999.99;
                db.CarrerProfiles.AddObject(career);
                db.SaveChanges();
            }
            Response.Write(String.Format("OK"));
        }
    }
}