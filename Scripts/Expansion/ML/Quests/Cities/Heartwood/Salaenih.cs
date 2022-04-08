using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Salaenih : MondainQuester
    {
        [Constructible]
        public Salaenih()
            : base("Salaenih", "the Expeditionist")
        {
            SetSkill(SkillName.Meditation, 60.0, 83.0);
            SetSkill(SkillName.Focus, 60.0, 83.0);
        }

        public Salaenih(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests => new Type[]
                {
                    typeof(WarriorCasteQuest),
                    typeof(ShakingThingsUpQuest),
                    typeof(ArachnophobiaQuest),
                    typeof(SquishyQuest),
                    typeof(BigJobQuest),
                    typeof(VoraciousPlantsQuest),
                    typeof(SpecimensQuest),
                    typeof(ColdHeartedQuest),
                    typeof(MiniSwampThingQuest),
                    typeof(AnimatedMonstrosityQuest)
                };
        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = true;
            Race = Race.Elf;

            Hue = 0x851D;
            HairItemID = 0x2FCD;
            HairHue = 0x324;
        }

        public override void InitOutfit()
        {
            AddItem(new ElvenBoots());
            AddItem(new WarCleaver());

            Item item;

            item = new WoodlandLegs();
            item.Hue = 0x1BB;
            AddItem(item);

            item = new WoodlandArms();
            item.Hue = 0x1BB;
            AddItem(item);

            item = new WoodlandChest();
            item.Hue = 0x1BB;
            AddItem(item);

            item = new WoodlandBelt();
            item.Hue = 0x597;
            AddItem(item);

            item = new VultureHelm();
            item.Hue = 0x1BB;
            AddItem(item);
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
