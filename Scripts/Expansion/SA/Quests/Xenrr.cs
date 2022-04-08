using System;
using Server;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Xenrr : MondainQuester
    {
        public override Type[] Quests => new Type[]
            {
                typeof( ScrapingtheBottom )
            };

        [Constructible]
        public Xenrr()
            : base()
        {
            Name = "Xenrr";
        }

        public override void InitBody()
        {
            HairItemID = 0x2044;//
            HairHue = 1153;
            FacialHairItemID = 0x204B;
            FacialHairHue = 1153;
            Body = 723;
            Blessed = true;
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());
            AddItem(new Boots());
            AddItem(new LongPants(0x6C7));
            AddItem(new FancyShirt(0x6BB));
            AddItem(new Cloak(0x59));
        }

        public Xenrr(Serial serial)
            : base(serial)
        {
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
