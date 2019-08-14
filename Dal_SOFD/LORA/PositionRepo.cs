using System.Linq;

namespace Dal_SOFD.LORA
{
    public class PositionRepo
    {
        private LoraDataContext c;

        public PositionRepo(string constr)
        {
            c = new LoraDataContext(constr);
        }

        public IQueryable<v_ad_user_creation> Query => c.v_ad_user_creations;
    }
}
