using System.Xml;
using System;

namespace FrontolParser.Xpos.Entities
{
    public class UnitBase : TypeBase
    {
        public bool IsEducation;
        public bool IsSelected;
        public string MainPath;
        public string LogPath;

        private UnitBase(string name, string description) : base(name, description) { }
        private void SetPath(string main, string log)
        {
            if (main == "" || log == "") throw new Exception(); // todo select exception
            MainPath = main;
            LogPath = log;
        }

        internal static UnitBase Parse(XmlElement xnode)
        {
            var obj = new UnitBase(xnode.GetAttribute("name"), xnode.GetAttribute("description"))
            {
                IsEducation = xnode.GetAttribute("education") == "1",
                IsSelected = xnode.GetAttribute("selected") == "1"
            };
            string mainPath = "", logPath = "";
            foreach (XmlElement child in xnode.ChildNodes)
            {
                switch (child.Name)
                {
                    case "mainDB":
                        mainPath = child.GetAttribute("val");
                        break;
                    case "logDB":
                        logPath = child.GetAttribute("val");
                        break;
                    default:
                        throw new Exception(); // todo select exception
                }
            }
            obj.SetPath(mainPath, logPath);
            return obj;
        }
    }
}
