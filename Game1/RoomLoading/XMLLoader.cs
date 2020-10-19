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
        private XmlNodeList items;
        private XmlNodeList projectiles;
        private XmlNodeList enemies;
        private XmlNodeList iteractEnviornment;
        private XmlNodeList nonInteractEnviornment;
        public XMLLoader(String fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            items = xmlDoc.GetElementsByTagName("Item");
            projectiles = xmlDoc.GetElementsByTagName("Projectile");
            enemies = xmlDoc.GetElementsByTagName("Enemy");
            iteractEnviornment = xmlDoc.GetElementsByTagName("InteractEnviornment");
            nonInteractEnviornment = xmlDoc.GetElementsByTagName("NonInteractEnviornment");
        }

        public XmlNodeList getItemNodes()
        {
            return items;
        }

        public XmlNodeList getProjectileNodes()
        {
            return projectiles;
        }
        public XmlNodeList getEnemyNodes()
        {
            return enemies;
        }
        public XmlNodeList getInteractEnviornmentNodes()
        {
            return iteractEnviornment;
        }
        public XmlNodeList getNonInteractEnviornmentNodes()
        {
            return nonInteractEnviornment;
        }

    }
}
