using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ProjektSki.DAL
{
    public interface IXMLService
    {
        public void Add(string messeage);
        public List<string> Get();
    }
    public class XMLService : IXMLService
    {
        XmlDocument db = new XmlDocument();
        string xmlDB_path;
        public XMLService(IConfiguration configuration)
        {
            xmlDB_path = configuration.GetConnectionString("XMLFile");
            db.Load(xmlDB_path);
        }
        public void Add(string messeage)
        {
            XmlNodeList xmlNodeList = db.SelectNodes("/log/messeage");
            int id = xmlNodeList.Count + 1;

            XDocument xmlDoc = XDocument.Load(xmlDB_path);

            xmlDoc.Element("log").Add(
                new XElement("messeage", new XAttribute("id", id.ToString()),
                new XElement("denied",messeage)
                ));

            xmlDoc.Save(xmlDB_path);
        }
        private void OpenXmlBase()
        {
            db.Load("DATA/AccessDeniedLog.xml");
        }
        public string NodeToSTring(XmlNode node)
        {
            return node["denied"].InnerText;
            
        }
        public List<string> Get()
        {
            db.Load("DATA/AccessDeniedLog.xml");
            List<string> logs = new List<string>();
            XmlNodeList xmlNodeList = db.SelectNodes("/log/messeage");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                logs.Add(NodeToSTring(xmlNode));
            }
            db.Save("DATA/AccessDeniedLog.xml");
            return logs;
        }
    }
}
