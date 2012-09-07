﻿using System;
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

public partial class usercontrols_BattleTagSearch : System.Web.UI.UserControl
{
    DynamicNode currentNode = new DynamicNode(Node.getCurrentNodeId());
    DynamicNode battletagProfile;
    D3API api;
    D3pServices d3pServices = D3pServices.Instance;
    List<object> profileHeroes;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblSearchStatus.Text = "";
        
    }
    protected void btnSearchBattleTag_Click(object sender, EventArgs e)
    {
        bool debug = true;
        if (debug)
        {
            //set this if you are debuging 
            lblSearchStatus.Text = "Sorry this service is not yet avalible yet, check back soon";
        }
        else
        {

            /*search order
             * 1. first it will look for the node that match the battletag
             * 2. if the node dosen't match it will start to fetch the profile using the blizzard diablo 3 web api
             * 3. else it fails and a text is written out
             */
            if (currentNode.Descendants(BattleTagProfile.documentTypeAlias).Items.Any(x => x.Name == TxtbSearchBattleTag.Text))
            {
                battletagProfile = currentNode.Descendants(x => x.NodeTypeAlias == BattleTagProfile.documentTypeAlias && x.Name == TxtbSearchBattleTag.Text).Items.First();
                Response.Redirect(currentNode.Descendants(ProfilePage.documentTypeAlias).Items.First().Url + "?id=" + battletagProfile.Id);

            }
            else
            {


                dynamic careerProfile = null;
                string battleTag = null;
                try
                {
                    api = new D3API(HostName.en_GB);
                    careerProfile = api.getCareerProfile(TxtbSearchBattleTag.Text);
                    battleTag = careerProfile.battleTag;
                }
                catch (Exception exception)
                {
                    string d = exception.ToString();
                    lblSearchStatus.Text = "Sorry we could't find your profile, please check if your spelled your battletag correct!";
                }


                if (!string.IsNullOrWhiteSpace(battleTag))
                {

                    User admin = User.GetUser(0);
                    Document profilesDoc = new Document(1060);

                    Document createdProfile = d3pServices.CreateDiablo3Profile(profilesDoc, careerProfile, admin);
                    if (createdProfile.Published)
                    {
                        Response.Redirect(currentNode.Descendants(ProfilePage.documentTypeAlias).Items.First().Url + "?id=" + createdProfile.Id);


                        //section moved to the heroes page instead because it took to many resourceses to doing at one time


                        //List<dynamic> heroes = careerProfile.heroes;
                        //if (heroes.Count > 0)
                        //{
                        //    int i = 1;
                        //    foreach (Dictionary<string, dynamic> hero in heroes)
                        //    {
                        //        foreach (var pair in hero)
                        //        {
                        //            if (pair.Key == "id")
                        //            {
                        //                var heroProfile = api.getHeroProfile(battleTag, pair.Value);
                        //                Document createdHero = d3pServices.CreateDiablo3Hero(createdProfile, heroProfile, admin);
                        //                if (createdHero.Published)
                        //                {
                        //                    d3pServices.CreateDiablo3HeroSkills(createdHero, admin);
                        //                    i++;
                        //                    if (i == heroes.Count + 1)
                        //                    {
                        //                        Response.Redirect(currentNode.Descendants(ProfilePage.documentTypeAlias).Items.First().Url + "?id=" + createdProfile.Id);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                    }


                }
                else
                {
                    lblSearchStatus.Text = "Sorry we could't find your profile, please check if your spelled your battletag correct!";
                }
            }
        }
    }
}