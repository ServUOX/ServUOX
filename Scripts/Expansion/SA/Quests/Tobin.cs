using Server;
using System;
using Server.Engines.Quests;
using Server.Items;

namespace Server.Mobiles
{
    public class Tobin : MondainQuester
    {
        [Constructible]
        public Tobin()
            : base("Fiddling Tobin", "the Tinkerer")
        {
        }

        public override void Advertise()
        {
            Say(1094984); // Hail traveler.  Come here a moment.  I want to ask you somethin'.
        }

        public Tobin(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(DoneInTheNameOfTinkeringQuest)
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
            AddItem(new Shirt(0x743));
            AddItem(new ShortPants(0x485));
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
