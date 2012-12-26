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
using umbraco.NodeFactory;

public partial class usercontrols_HeroesPage : System.Web.UI.UserControl
{
    private DynamicNode profile;
    private DynamicNode currentNode;
    private string heroPageUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        currentNode = new DynamicNode(Node.getCurrentNodeId());
        heroPageUrl = "/profile/heroes/hero.aspx";

        if (!Page.IsPostBack && !string.IsNullOrEmpty(Request.QueryString["id"]))
        {

            
            profile = new DynamicNode(Convert.ToInt32(Request.QueryString["id"]));

            //check if profile has heroes else show the download heroes panel
            if (!profile.Descendants(x => x.NodeTypeAlias == Hero.documentTypeAlias).IsNull())
            {
                rViewHeroes.DataSource = profile.Descendants(Hero.documentTypeAlias);
                rViewHeroes.DataBind();
            }
            else
            {

            }
        }
    }

    protected void rViewHeroes_ItemDataBound(object sender, RepeaterItemEventArgs evt)
    {
        if (evt.Item.ItemType == ListItemType.Item || evt.Item.ItemType == ListItemType.AlternatingItem)
        {
            try
            {
                DynamicNode hero = (DynamicNode)evt.Item.DataItem;

                ((HyperLink)evt.Item.FindControl("hplHero")).NavigateUrl = heroPageUrl + "?id=" + hero.Id;
                ((Image)evt.Item.FindControl("imgHeroDificulty")).ImageUrl = D3pUtilities.GetHeroDificulty(hero.GetPropertyValue(Hero.heroHardcore));
                ((Label)evt.Item.FindControl("lblHeroName")).Text = hero.GetPropertyValue(Hero.heroName);
                ((Literal)evt.Item.FindControl("lblHeroLevel")).Text = hero.GetPropertyValue(Hero.heroLevel);
                ((Image)evt.Item.FindControl("imgHeroIcon")).ImageUrl = D3pUtilities.GetHeroIcon(hero.GetPropertyValue(Hero.heroClass));

            }
            catch (Exception exc)
            {

                string b = exc.ToString();
                Response.Write(exc.StackTrace);
            }
        }

    }
}