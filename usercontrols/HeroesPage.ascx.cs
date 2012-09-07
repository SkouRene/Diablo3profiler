using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.MacroEngines;
using D3APIdotNet;
using D3pCom.Generics;
using D3pCom.Utilities;

public partial class usercontrols_HeroesPage : System.Web.UI.UserControl
{
    private DynamicNode profile;
    private int profileId;

    protected void Page_Load(object sender, EventArgs e)
    {
        profileId = Convert.ToInt32(Request.QueryString["id"]);
        profile = new DynamicNode(profileId);

        //check if profile has heroes else start to fetch to heroes from blizzard
        if(!profile.Descendants(x => x.NodeTypeAlias == Hero.documentTypeAlias).IsNull())
        {

        }
    }
}