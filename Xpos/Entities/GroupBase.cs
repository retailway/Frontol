using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace RetailWay.Frontol.Xpos.Entities
{
    public class GroupBase : TypeBase, IEnumerable<TypeBase>
    {
        private TypeBase[] Children;

        private GroupBase(string name, string description) : base(name, description) { }
        internal static GroupBase Parse(XmlElement xnode)
        {
            var obj = new GroupBase(xnode.GetAttribute("name"), xnode.GetAttribute("description"))
            {
                Children = new TypeBase[xnode.ChildNodes.Count]
            };
            for (int i = 0; i < obj.Children.Length; i++)
            {
                var child = (XmlElement)xnode.ChildNodes[i];
                if (child.GetAttribute("type") == "1")
                    obj.Children[i] = UnitBase.Parse(child);
                else
                    obj.Children[i] = Parse(child);
            }
            return obj;
        }
        public IEnumerator GetEnumerator() => Children.GetEnumerator();
        IEnumerator<TypeBase> IEnumerable<TypeBase>.GetEnumerator() => 
            ((IEnumerable<TypeBase>)Children).GetEnumerator();
    }
}
