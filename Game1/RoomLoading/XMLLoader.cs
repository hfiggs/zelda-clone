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

        public XMLLoader(String fileName)
        {
            this.xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
        }

        public XmlNodeList getItemNodes()
        {
            return xmlDoc.GetElementsByTagName(itemTag); 
        }

        public XmlNodeList getProjectileNodes()
        {
            return xmlDoc.GetElementsByTagName(projectileTag); ;
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
