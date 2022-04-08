using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Sutek : MondainQuester
    {
        private static Type[] m_Quests = new Type[] { typeof(PerfectTimingQuest) };
        public override Type[] Quests => m_Quests;

        [Constructible]
        public Sutek()
            : base("Sutek", "the Mage")
        {
            TalkTimer();
        }

        public Sutek(Serial serial)
            : base(serial)
        {
        }

        public override void Advertise()
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Race = Race.Human;

            Hue = 0x840D;

            HairItemID = 0x203C; // Long Hair
            HairHue = 0x835;
            FacialHairItemID = 0x2040; // goatee
            FacialHairHue = 0x835;
        }

        public override void InitOutfit()
        {
            AddItem(new Sandals());
            AddItem(new TattsukeHakama(0x528));
            AddItem(new WizardsHat(0x528));
            AddItem(new Tunic(0x528));
        }

        public void TalkTimer()
        {
            Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(15.0), new TimerCallback(
                delegate
                {
                    Say(1113224 + Utility.Random(15));
                }
            ));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            TalkTimer();
        }
    }
}
