using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D3pCom.Generics;
using umbraco.MacroEngines;
using umbraco.NodeFactory;

namespace D3pCom.Utilities
{
    /// <summary>
    /// Summary description for D3pUtilities
    /// </summary>
    public class D3pUtilities
    {
        

        private D3pUtilities()
        {
        }

        public static string GetHeroIcon(string herodificulty)
        {
            string heroIcon = null;
            switch (herodificulty)
            {
                case "monk":
                    heroIcon = "/images/monk100.png";
                    break;
                case "barbarian":
                    heroIcon = "/images/barb100.png";
                    break;
                case "demon-hunter":
                    heroIcon = "/images/dh100.png";
                    break;
                case "wizard":
                    heroIcon = "/images/wic100.png";
                    break;
                case "witch-doctor":
                    heroIcon = "/images/wd100.png";
                    break;
            }

            return heroIcon;
        }

        public static string GetHeroDificulty(string herohardcore)
        {
            string heroDificulty = "/images/normal100.png";
            if (herohardcore == "true")
            {
                heroDificulty = "/images/hardcore100.png";
                return heroDificulty;
            }
            else
            {
                return heroDificulty;
            }
        }
        public static string GetBattleTagByNodeID(int id)
        {
            DynamicNode profile = new DynamicNode(id);
            string battletag = null;

            if(profile.NodeTypeAlias == BattleTagProfile.documentTypeAlias)
            {
                battletag = profile.Name;
                return battletag;
            }
            else 
            {
                return battletag;
            }

            
        }
    }
}