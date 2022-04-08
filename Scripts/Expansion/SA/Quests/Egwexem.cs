using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Egwexem : MondainQuester
    {
        [Constructible]
        public Egwexem()
            : base("Egwexem", "the Noble")
        {
        }

        public Egwexem(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(RumorsAboundQuest)
                };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            CantWalk = true;
            Body = 666;
            HairItemID = 16987;
            HairHue = 1801;
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());
            AddItem(new GargishClothChest());
            AddItem(new GargishClothKilt());
            AddItem(new GargishClothLegs(Utility.RandomNeutralHue()));
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

    public class EgwexemWrit : Item
    {
        [Constructible]
        public EgwexemWrit()
            : base(0x0E34)
        {
            //Hue = 3;
        }

        public EgwexemWrit(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1112520;
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
