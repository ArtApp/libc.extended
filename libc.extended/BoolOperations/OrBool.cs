using System.Linq;
namespace libc.extended.BoolOperations {
    public class OrBool : BoolBag<OrBool> {
        public override bool Value() {
            return list.Aggregate(false, (sofar, item) => sofar || item.Value());
        }
    }
}