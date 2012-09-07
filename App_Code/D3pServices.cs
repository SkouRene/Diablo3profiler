using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.cms.businesslogic.web;
using umbraco.BusinessLogic;
using D3pCom.Utilities;
using D3pCom.Generics;
using ImpromptuInterface;
using D3APIdotNet;

/// <summary>
/// Summary description for D3pServices
/// </summary>
/// 
namespace D3pCom.Generics
{
    public class D3pServices
    {
        #region singleton

        private static D3pServices instance;

        public D3pServices()
        {   
        }

        public static D3pServices Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new D3pServices();
                }
                return instance;
            }
        }

        #endregion

        #region public methods

        public Document CreateDiablo3Profile(Document profiles, dynamic careerprofile, User author)
        {
            string battleTag = careerprofile.battleTag;
            Document profile = CreateAndPublishDocument(battleTag, BattleTagProfile.documentTypeAlias, author, profiles.Id);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.lastUpdated, careerprofile.lastUpdated);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.lastPlayedHero,careerprofile.lastHeroPlayed);
            SetDocumentPropertyOrDefault(profile,BattleTagProfile.monsters,careerprofile.kills.monsters);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.elites, careerprofile.kills.elites);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.hardcoreMonsters, careerprofile.kills.hardcoreMonsters);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.timePlayedBarbarian, careerprofile.timePlayed.barbarian);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.timePlayedDemonHunter, Convert.ToString(Impromptu.InvokeGet(careerprofile.timePlayed, "demon-hunter")));
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.timePlayedMonk, careerprofile.timePlayed.monk);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.timePlayedWicthDoctor, Convert.ToString(Impromptu.InvokeGet(careerprofile.timePlayed, "witch-doctor")));
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.timePlayedWizard, careerprofile.timePlayed.wizard);
            SetDocumentPropertyOrDefault(profile, BattleTagProfile.numberOfHeroes, Convert.ToString(careerprofile.heroes.Count));
            UmbracoUtilities.SaveAndPublish(profile);
            return profile;
        }

        public Document CreateDiablo3Hero(Document profile,dynamic heroprofile,User author)
        {
            string heroId = Convert.ToString(heroprofile.id);
            Document hero = CreateAndPublishDocument(heroId, Hero.documentTypeAlias, author, profile.Id);
            SetDocumentPropertyOrDefault(hero,Hero.heroClass,Impromptu.InvokeGet(heroprofile,"class"));
            SetDocumentPropertyOrDefault(hero, Hero.heroHardcore, heroprofile.hardcore);
            SetDocumentPropertyOrDefault(hero, Hero.heroId,heroId);
            SetDocumentPropertyOrDefault(hero, Hero.heroLevel, heroprofile.level);
            SetDocumentPropertyOrDefault(hero, Hero.heroName, heroprofile.name);
            SetDocumentPropertyOrDefault(hero, Hero.isCreatedFirstTime, true);
            SetDocumentPropertyOrDefault(hero, Hero.dead, heroprofile.dead);
            SetDocumentPropertyOrDefault(hero, Hero.lastUpdated, Convert.ToString(Impromptu.InvokeGet(heroprofile,"last-updated")));
            SetDocumentPropertyOrDefault(hero, Hero.paragonLevel, heroprofile.paragonLevel);
            SetDocumentPropertyOrDefault(hero, Hero.heroGender, heroprofile.gender);
            SetDocumentPropertyOrDefault(hero, Hero.heroKillsElites, heroprofile.kills.elites);
            UmbracoUtilities.SaveAndPublish(hero);
            return hero;
        }
        public void CreateDiablo3HeroSkills(Document hero, User author)
        {
            Document skills = CreateAndPublishDocument("skills", Skills.documentTypeAlias, author, hero.Id);
            
        }

        public Document UpdateDiablo3Hero(Document hero, dynamic heroprofile)
        {
            //generel
            SetDocumentPropertyOrDefault(hero, Hero.isCreatedFirstTime, false);
            SetDocumentPropertyOrDefault(hero, Hero.heroGender, heroprofile.gender);
            SetDocumentPropertyOrDefault(hero, Hero.heroKillsElites, heroprofile.kills.elites);

            //skills

            UmbracoUtilities.SaveAndPublish(hero);
            return hero;
        }

        #endregion

        #region private methods

        private Document CreateAndPublishDocument(string name, string contentTypeAlias, User author, int parentId)
        {
            Document document = Document.MakeNew(name, DocumentType.GetByAlias(contentTypeAlias), author, parentId);
            UmbracoUtilities.SaveAndPublish(document);

            return document;
        }

        private Document CreateAndPublishDocument(string name, string contentTypeAlias, User author, int parentId, int templateId)
        {
            Document document = Document.MakeNew(name, DocumentType.GetByAlias(contentTypeAlias), author, parentId);
            UmbracoUtilities.SaveAndPublish(document);

            return document;
        }

        private void UnpublishAndTrashDocument(Document document)
        {
            if (document.Published)
            {
                document.UnPublish();
                umbraco.library.UnPublishSingleNode(document.Id);
            }
            document.delete();
        }

        private void SetDocumentPropertyOrDefault(Document document, string propertyAlias, string propertyValue)
        {
            if (document != null && !string.IsNullOrWhiteSpace(propertyValue) && !string.IsNullOrWhiteSpace(propertyAlias))
            {
                document.getProperty(propertyAlias).Value = propertyValue;
            }
        }

        private void SetDocumentPropertyOrDefault(Document document, string propertyAlias, object propertyValue)
        {
            if (document != null && propertyValue != null && !string.IsNullOrWhiteSpace(propertyAlias))
            {
                document.getProperty(propertyAlias).Value = propertyValue;
            }
        }

        private void ValidateRequiredArgument(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(string.Format("Argument {0} cannot be null.", argumentName));
        }

        private void ValidateRequiredStringArgument(string argumentValue, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
                throw new ArgumentException(string.Format("Argument {0} cannot be null, empty string or whitespace.", argumentName));
        }

        private void ValidateDocumentType(Document document, string expectedDocumentTypeAlias)
        {
            if (document.ContentType.Alias != expectedDocumentTypeAlias)
                throw new ArgumentException(string.Format("Given document is of not correct document type. Expecting '{0}', given '{1}'.", expectedDocumentTypeAlias, document.ContentType.Alias));
        }

        private void ValidateDocumentCreator(User author)
        {
            if (author.UserType.Alias != BackendUserAliases.userTypeAlias)
                throw new ArgumentException(string.Format("Argument author must be of user type: {0}, given: {1}", BackendUserAliases.userTypeAlias, author.UserType.Alias));
        }

        #endregion
    }
}