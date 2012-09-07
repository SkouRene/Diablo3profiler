using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using D3pCom.Generics;
using umbraco.NodeFactory;
using umbraco.MacroEngines;
using D3APIdotNet;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using ImpromptuInterface;

public partial class usercontrols_Methods : System.Web.UI.UserControl
{
    DynamicNode currentNode = new DynamicNode(Node.getCurrentNodeId());
    DynamicNode battletagProfile;
    D3API api;
    D3pServices d3pServices = D3pServices.Instance;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            api = new D3API(HostName.en_GB);
            dynamic careerProfile = api.getCareerProfile(txtbBattletag.Text);
            foreach (Dictionary<string,dynamic> hero in careerProfile.heroes)
            {
                foreach (var pair in hero)
                {
                    if (pair.Key == "id")
                    {
                        dynamic heroProfile = api.getHeroProfile(careerProfile.battleTag, pair.Value);
                        Document heroDocument = d3pServices.CreateDiablo3Hero(new Document(2354), heroProfile, User.GetUser(0));
                    }
                    
                }
            }
        }catch(Exception errorEvent)
        {
            string error = errorEvent.ToString();
            lblTestMethods.Text = error;
        }
               
                
    }
        
    
}