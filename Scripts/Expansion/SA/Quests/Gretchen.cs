using System;
using Server;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Gretchen : MondainQuester
    {
        public override Type[] Quests => new Type[]
        {
            typeof( Curiosities )
        };

        [Constructible]
        public Gretchen() : base("Gretchen", "the Alchemist")
        {
        }

        public Gretchen(Serial serial) : base(serial)
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = true;
            Race = Race.Elf;

            Hue = 33767;
            HairItemID = 0x2047;
            HairHue = 0x465;

            CantWalk = true;
            Direction = Direction.East;
        }

        public override void InitOutfit()
        {
            SetWearable(new Backpack());
            SetWearable(new Shoes(1886));
            SetWearable(new FemaleElvenRobe(443));

            SetWearable(new QuarterStaff());
        }

        public override void Advertise()
        {
            Say(1094977);  // What's this I see?  A traveler fit to service me?
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0)
            {
                Female = true;
                Race = Race.Elf;

                Hue = 33767;
                HairItemID = 0x2047;
                HairHue = 0x465;

                CantWalk = true;
                Direction = Direction.East;

                Item item = FindItemOnLayer(Layer.Cloak);
                if (item != null)
                    item.Delete();

                item = FindItemOnLayer(Layer.MiddleTorso);
                if (item != null)
                    item.Delete();

                item = FindItemOnLayer(Layer.MiddleTorso);
                if (item != null)
                    item.Hue = 1886;

                SetWearable(new FemaleElvenRobe(443));
                SetWearable(new QuarterStaff());
            }
        }
    }
}
