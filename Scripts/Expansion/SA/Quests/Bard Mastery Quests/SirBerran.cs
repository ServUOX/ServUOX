using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class SirBerran : MondainQuester
    {
        [Constructible]
        public SirBerran()
        {
            Name = "Sir Berran";
            Title = "the Song Weilder";
        }

        public SirBerran(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(WieldingTheSonicBladeQuest),
                };

        public override void InitBody()
        {
            InitStats(125, 100, 25);

            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (IsInvulnerable && !Core.AOS)
                NameHue = 0x35;

            Female = false;
            Body = 0x190;

            SetWearable(new ChainChest(), 0x2BF);
            SetWearable(new Shoes());
            SetWearable(new ShortPants());
            SetWearable(new Halberd());
            SetWearable(new BodySash(0x355));
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
