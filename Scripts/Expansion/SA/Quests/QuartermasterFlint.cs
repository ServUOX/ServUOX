using Server;
using System;
using Server.Engines.Quests;
using Server.Items;

namespace Server.Mobiles
{
    public class QuartermasterFlint : MondainQuester
    {
        [Constructible]
        public QuartermasterFlint()
            : base("Quartermaster Flint", "")
        {
        }

        public QuartermasterFlint(Serial serial)
            : base(serial)
        {
        }

        public override void Advertise()
        {
            Say(Utility.RandomBool() ? 1094959 : 1094969); // Keep an eye pealed, traveler, thieves be afoot!
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(ThievesBeAfootQuest)
                };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Race = Race.Human;

            Hue = 0x8418;
            HairItemID = 0x2046;
            HairHue = 0x466;
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());
            AddItem(new Shoes(0x743));
            AddItem(new LongPants());
            AddItem(new FancyShirt());
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
        }
    }
}
