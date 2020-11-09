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

        public XMLLoader(String fileName)
        {
            this.xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
        }

        public XmlNodeList getItemNodes()
        {
            return xmlDoc.GetElementsByTagName("Item"); 
        }

        public XmlNodeList getProjectileNodes()
        {
            return xmlDoc.GetElementsByTagName("Projectile"); ;
        }
        public XmlNodeList getEnemyNodes()
        {
            return xmlDoc.GetElementsByTagName("Enemy"); ;
        }
        public XmlNodeList getInteractEnviornmentNodes()
        {
            return xmlDoc.GetElementsByTagName("InteractEnviornment"); ;
        }
        public XmlNodeList getNonInteractEnviornmentNodes()
        {
            return xmlDoc.GetElementsByTagName("NonInteractEnviornment"); ;
        }

        public XmlNodeList getAmbientSounds()
        {
            return xmlDoc.GetElementsByTagName("Sounds");
        }
    }
}
