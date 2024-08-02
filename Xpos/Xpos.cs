using System.Xml;
using RetailWay.Frontol.Xpos.Entities;
using System.Collections.Generic;
using System.Data.SQLite;

namespace RetailWay.Frontol.Xpos
{
    public static partial class Xpos
    {
        private static TypeBase[] dbs;
        private static UnitBase _db;
        public static UnitBase selectedDb { get => _db ?? (_db = GetSelectedBases()); }
        #region Константы
        private static string _confPath = @"C:\ProgramData\ATOL\fxpos\Frontol.xml";
        
        #endregion
        public static void Init()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(_confPath);
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                dbs = new TypeBase[xRoot.ChildNodes.Count];
                for (int i = 0; i < dbs.Length; i++)
                {
                    var xnode = (XmlElement)xRoot.ChildNodes[i];
                    if (xnode.GetAttribute("type") == "1")
                        dbs[i] = UnitBase.Parse(xnode);
                    else
                        dbs[i] = GroupBase.Parse(xnode);
                }
            }
        }

        public static UnitBase GetSelectedBases(GroupBase group = null)
        {
            foreach(var db in (IEnumerable<TypeBase>)group ?? dbs)
            {
                if(db is GroupBase gr)
                {
                    if (GetSelectedBases(gr) is UnitBase unit)
                        return unit;
                }
                else if(db is UnitBase un)
                {
                    if (un.IsSelected)
                        return un;
                }
            }
            return null;
        }
    }
}
