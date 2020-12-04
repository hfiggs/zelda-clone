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

        // xml Tag Names
        private const string itemTag = "Item", projectileTag = "Projectile", enemyTag = "Enemy", interactEnviornmentTag = "InteractEnviornment", nonInteractEnviornment = "NonInteractEnviornment", puzzleTag = "Puzzle", soundsTag = "Sounds";

        public XMLLoader(String fileName, int difficulty)
        {
            this.xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            switch (difficulty)
            {
                case 0:
                    if(xmlDoc.GetElementsByTagName("Medium").Count > 0)
                        xmlDoc.DocumentElement.RemoveChild(xmlDoc.DocumentElement.SelectSingleNode("Medium"));
                    goto case 1;
                case 1:
                    if (xmlDoc.GetElementsByTagName("Hard").Count > 0)
                        xmlDoc.DocumentElement.RemoveChild(xmlDoc.DocumentElement.SelectSingleNode("Hard"));
                    break;
                default:
                    if (xmlDoc.GetElementsByTagName("Medium").Count > 0)
                        xmlDoc.DocumentElement.RemoveChild(xmlDoc.DocumentElement.SelectSingleNode("Medium"));
                    break;
               
            }


        }

        public XmlNodeList getItemNodes()
        {
            return xmlDoc.GetElementsByTagName(itemTag);
        }

        public XmlNodeList getProjectileNodes()
        {
            return xmlDoc.GetElementsByTagName(projectileTag);
        }
        public XmlNodeList getEnemyNodes()
        {
            return xmlDoc.GetElementsByTagName(enemyTag); ;
        }
        public XmlNodeList getInteractEnviornmentNodes()
        {
            return xmlDoc.GetElementsByTagName(interactEnviornmentTag); ;
        }
        public XmlNodeList getNonInteractEnviornmentNodes()
        {
            return xmlDoc.GetElementsByTagName(nonInteractEnviornment); ;
        }
        public XmlNodeList getPuzzleNodes()
        {
            return xmlDoc.GetElementsByTagName(puzzleTag);
        }
        public XmlNodeList getAmbientSounds()
        {
            return xmlDoc.GetElementsByTagName(soundsTag);
        }
    }
}
