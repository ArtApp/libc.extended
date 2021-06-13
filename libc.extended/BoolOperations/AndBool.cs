using System.Linq;

namespace libc.extended.BoolOperations
{
    public class AndBool : BoolBag<AndBool>
    {
        public override bool Value()
        {
            return list.Aggregate(true, (sofar, item) => sofar && item.Value());
        }
    }
}