using System;
using Server.Engines.Quests;
using Server.Items;

namespace Server.Mobiles
{
    public class Danoel : MondainQuester
    {
        [Constructible]
        public Danoel()
            : base("Danoel", "the Metal Weaver")
        {
        }

        public Danoel(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(ReptilianDentistQuest),
                    typeof(TickTockQuest),
                    typeof(NothingFancyQuest),
                    typeof(InstrumentOfWarQuest),
                    typeof(TheShieldQuest),
                    typeof(TheGlassEyeQuest),
                    typeof(MusicToMyEarsQuest),
                    typeof(LazyHumansQuest),
                    typeof(InventiveToolsQuest),
                    typeof(LeatherAndLaceQuest),
                    typeof(SpringCleaningQuest),
                    typeof(CowardsQuest),
                    typeof(TokenOfLoveQuest)
                };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = true;
            CantWalk = true;
            Race = Race.Elf;

            Hue = 0x8579;
            HairItemID = 0x2FC0;
            HairHue = 0x206;
        }

        public override void InitOutfit()
        {
            AddItem(new ElvenBoots(0x901));
            AddItem(new ElvenPants(0x386));
            AddItem(new ElvenShirt(0x71D));
            AddItem(new SmithHammer());
            AddItem(new RoyalCirclet());
            AddItem(new FullApron(0x1BB));
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
