using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Ortlem : MondainQuester
    {
        private static Type[] m_Quests = new Type[] { typeof(MysticsJourneyQuest) };
        public override Type[] Quests => m_Quests;

        public override bool IsActiveVendor => true;

        public override void InitSBInfo()
        {
            SBInfos.Add(new SBMystic());
        }

        [Constructible]
        public Ortlem()
            : base("Ortlem", "the Mystic")
        {
            SetSkill(SkillName.EvalInt, 65.0, 90.0);
            SetSkill(SkillName.Meditation, 65.0, 90.0);
            SetSkill(SkillName.MagicResist, 65.0, 90.0);
            SetSkill(SkillName.Mysticism, 65.0, 90.0);
        }

        public Ortlem(Serial serial)
            : base(serial)
        {
        }

        public override void Advertise()
        {
            Say(1112562); // Become adept in Mysticism. Help save Ter-Mur!
        }

        public override void InitBody()
        {
            Female = false;
            CantWalk = true;
            Race = Race.Gargoyle;

            Hue = 0x86ED;
            HairItemID = 0x4258;
            HairHue = 0x38A;
        }

        public override void InitOutfit()
        {
            AddItem(new GlassStaff());
            AddItem(new GargishClothChest(0x64F));
            AddItem(new GargishClothArms(0x64F));
            AddItem(new GargishClothKilt(0x643));
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
