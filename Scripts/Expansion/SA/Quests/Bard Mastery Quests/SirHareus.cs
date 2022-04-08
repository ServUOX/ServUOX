using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class SirHareus : MondainQuester
    {
        [Constructible]
        public SirHareus()
        {
            Name = "Sir Hareus";
            Title = "the Battle Rouser";
        }

        public SirHareus(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(IndoctrinationOfABattleRouserQuest),
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

            SetWearable(new ChainChest(), 0x30A);
            SetWearable(new Shoes(GetShoeHue()));
            SetWearable(new LongPants(GetRandomHue()));
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
