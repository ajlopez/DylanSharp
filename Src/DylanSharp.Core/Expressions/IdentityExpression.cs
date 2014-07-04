namespace DylanSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IdentityExpression : BinaryExpression
    {
        public IdentityExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue is System.ValueType)
                return leftvalue.Equals(rightvalue);
            if (rightvalue is System.ValueType)
                return rightvalue.Equals(leftvalue);

            return leftvalue == rightvalue;
        }
    }
}
