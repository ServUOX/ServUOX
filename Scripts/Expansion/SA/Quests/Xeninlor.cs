using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Xeninlor : MondainQuester
    {
        [Constructible]
        public Xeninlor()
            : base("Xeninlor", "the Security Advisor")
        {
        }

        public Xeninlor(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(GatheringOfEvidence)
                };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            CantWalk = true;
            Race = Race.Gargoyle;

            Body = 666;
            Utility.AssignRandomHair(this);
            Utility.AssignRandomFacialHair(this);
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());

            AddItem(new GargishClothChest(Utility.RandomNeutralHue()));
            AddItem(new GargishClothKilt(Utility.RandomNeutralHue()));
            AddItem(new GargishClothLegs(Utility.RandomNeutralHue()));
            AddItem(new SerpentStoneStaff());
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
