using System;
using System.Linq;
using System.Reflection;

namespace FrontolParser.Xpos.Entities
{
    public abstract class Transaction 
    {
        public abstract void Pull(int id);
        public static Transaction Parse(int id, int type)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            types = types.Where(t => t.BaseType == typeof(Transaction)).ToArray();
            var ty = types.First(t => t.GetCustomAttribute<TransactionTypeAttribute>().Types.Contains(type));
            var tr = (Transaction)Activator.CreateInstance(ty);
            tr.Pull(id);
            return tr;
        }
    }
}
