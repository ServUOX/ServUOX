using Server.Engines.Points;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Engines.Khaldun
{
    public class KhaldunRewardGump : BaseRewardGump
    {
        public KhaldunRewardGump(Mobile owner, PlayerMobile user)
            : base(owner, user, Rewards, 1158744)
        {
        }

        public override double GetPoints(Mobile m)
        {
            return PointsSystem.Khaldun.GetPoints(m);
        }

        public override void RemovePoints(double points)
        {
            PointsSystem.Khaldun.DeductPoints(User, points);
        }

        public static List<CollectionItem> Rewards { get; set; }

        public static void Initialize()
        {
            Rewards = new List<CollectionItem>
            {
                new CollectionItem(typeof(KhaldunFirstAidBelt), 0xA1F6, 1158681, 0, 30),
                new CollectionItem(typeof(MaskOfKhalAnkur), 0xA1C7, 1158701, 0, 50),
                new CollectionItem(typeof(PendantOfKhalAnkur), 0xA1C9, 1158731, 0, 50),
                new CollectionItem(typeof(SeekerOfTheFallenStarTitleDeed), 5360, 1155604, 0, 20),
                new CollectionItem(typeof(ZealotOfKhalAnkurTitleDeed), 5360, 1155604, 0, 30),
                new CollectionItem(typeof(ProphetTitleDeed), 5360, 1155604, 0, 50),
                new CollectionItem(typeof(CultistsRitualTome), 0xEFA, 1158717, 0, 50),
                new CollectionItem(typeof(SterlingSilverRing), 0x1F09, 1155606, 0, 50),
                new CollectionItem(typeof(TalonsOfEscaping), 0x41D8, 1155682, 0, 50),
                new CollectionItem(typeof(BootsOfEscaping), 0x1711, 1155607, 0, 50)
            };
        }

        public static bool IsTokunoDyable(Type t)
        {
            return Rewards.FirstOrDefault(item => item.Type == t) != null;
        }
    }
}
