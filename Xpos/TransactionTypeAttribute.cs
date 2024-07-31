using System;

namespace FrontolParser.Xpos
{
    internal class TransactionTypeAttribute: Attribute
    {
        public int[] Types;
        public TransactionTypeAttribute(params int[] types)
        {
            Types = types;
        }
    }
}
