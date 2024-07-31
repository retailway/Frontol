namespace FrontolParser.Xpos.Entities
{
    public abstract class TypeBase
    {
        public string Name;
        public string Description;

        public TypeBase(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
