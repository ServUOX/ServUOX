using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Prassel : MondainQuester
    {
        [Constructible]
        public Prassel()
            : base("Prassel", "the Security Liason")
        {
        }

        public Prassel(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(GatheringProof)
                };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Race = Race.Human;

            Body = 0x190;
            Utility.AssignRandomHair(this);
            Utility.AssignRandomFacialHair(this);
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());

            AddItem(new Tunic(Utility.RandomNeutralHue()));
            AddItem(new ShortPants(Utility.RandomNeutralHue()));
            AddItem(new Boots());
            AddItem(new Halberd());
        }

        public override bool CheckTerMur()
        {
            return false;
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
