using System;
using Server.Items;
using Server.Mobiles;
using Server.Spells.SkillMasteries;
using System.Collections.Generic;

namespace Server.Engines.Quests
{
    public class SirFelean : MondainQuester
    {
        [Constructible]
        public SirFelean()
        {
            Name = "Sir Felean";
            Title = "the Spirit Soother";
        }

        public SirFelean(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(TheBeaconOfHarmonyQuest),
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

            SetWearable(new ChainChest(), 0x35);
            SetWearable(new Shoes(GetShoeHue()));
            SetWearable(new LongPants());
            SetWearable(new Halberd());
            SetWearable(new BodySash(0x498));
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
