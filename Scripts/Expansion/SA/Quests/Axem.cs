using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Axem : MondainQuester
    {
        [Constructible]
        public Axem()
            : base("Axem", "the Curator")
        {
            SetSkill(SkillName.Meditation, 60.0, 83.0);
            SetSkill(SkillName.Focus, 60.0, 83.0);
        }

        public Axem(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new[]
                {
                    typeof (ABrokenVaseQuest),
                    typeof (PuttingThePiecesTogetherQuest),
                    typeof (YeOldeGargishQuest)
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

            AddItem(new GargishClothChest(Utility.RandomNeutralHue()));
            AddItem(new GargishClothKilt(Utility.RandomNeutralHue()));
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

            var version = reader.ReadInt();
        }
    }
}
