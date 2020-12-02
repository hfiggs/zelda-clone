using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.RoomLoading
{
    class XMLLoader
    {
        private XmlDocument xmlDoc { get; set; }
        private XmlNodeList levelDifficulty;

        // xml Tag Names
        private const string itemTag = "Item", projectileTag = "Projectile", enemyTag = "Enemy", interactEnviornmentTag = "InteractEnviornment", nonInteractEnviornment = "NonInteractEnviornment", puzzleTag = "Puzzle", soundsTag = "Sounds";

        public XMLLoader(String fileName, int difficulty)
        {
            this.xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            switch (difficulty)
            {
                case 2:
                    levelDifficulty = getEasyNodes();
                    break;
                case 1:
                    levelDifficulty = getMediumlNodes();
                    break;
                default:
                    levelDifficulty = getEasyNodes();
                    break;
               
            }


        }

        public XmlNodeList getEasyNodes()
        {
            return xmlDoc.GetElementsByTagName("Easy");
        }

        public XmlNodeList getMediumlNodes()
        {
            return (XmlNodeList)xmlDoc.GetElementsByTagName("Medium").Cast<XmlNode>().Concat<XmlNode>(getEasyNodes().Cast<XmlNode>());
        }

        public XmlNodeList getHardlNodes()
        {
            return (XmlNodeList) xmlDoc.GetElementsByTagName("Hard").Cast<XmlNode>().Concat<XmlNode>(getMediumlNodes().Cast<XmlNode>());
        }

        public XmlNodeList getItemNodes()
        {
            return levelDifficulty[0].SelectNodes(itemTag);
        }

        public XmlNodeList getProjectileNodes()
        {
            return levelDifficulty[0].SelectNodes(projectileTag);
        }
        public XmlNodeList getEnemyNodes()
        {
            return levelDifficulty[0].SelectNodes(enemyTag); ;
        }
        public XmlNodeList getInteractEnviornmentNodes()
        {
            return levelDifficulty[0].SelectNodes(interactEnviornmentTag); ;
        }
        public XmlNodeList getNonInteractEnviornmentNodes()
        {
            return levelDifficulty[0].SelectNodes(nonInteractEnviornment); ;
        }
        public XmlNodeList getPuzzleNodes()
        {
            return levelDifficulty[0].SelectNodes(puzzleTag);
        }
        public XmlNodeList getAmbientSounds()
        {
            return levelDifficulty[0].SelectNodes(soundsTag);
        }
    }
}
