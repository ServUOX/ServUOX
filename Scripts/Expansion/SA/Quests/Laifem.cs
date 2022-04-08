using System;
using Server.Items;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Laifem : MondainQuester
    {
        public override bool IsActiveVendor => true;

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBCarpets());
        }

        [Constructible]
        public Laifem() : base("Laifem", "the Weaver")
        {
        }

        public Laifem(Serial serial) : base(serial)
        {
        }

        public override void VendorBuy(Mobile from)
        {
            if (!(from is PlayerMobile) || !((PlayerMobile)from).CanBuyCarpets)
            {
                SayTo(from, 1113266); // I'm sorry, but I don't have any carpets to sell you yet.
                return;
            }

            base.VendorBuy(from);
        }

        public override void Advertise()
        {
        }

        private static Type[] m_Quests = new Type[] { typeof(ShearingKnowledgeQuest) };
        public override Type[] Quests => m_Quests;

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = true;
            CantWalk = true;

            Race = Race.Gargoyle;
            HairItemID = Race.RandomHair(true);
            Hue = Race.RandomSkinHue();
            HairHue = Race.RandomHairHue();
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

            int version = reader.ReadInt();
        }
    }

    public class Dermott : MondainQuester
    {
        public override Type[] Quests => null;

        [Constructible]
        public Dermott() : base("Dermott", "the Weaver")
        {
            SetSkill(SkillName.Magery, 60.0, 90.0);
            SetSkill(SkillName.EvalInt, 60.0, 90.0);
            SetSkill(SkillName.MagicResist, 60.0, 90.0);
            SetSkill(SkillName.Wrestling, 60.0, 90.0);
            SetSkill(SkillName.Meditation, 60.0, 90.0);
        }

        public Dermott(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Race = Race.Human;

            Hue = 0x83FC;
            HairItemID = 0x2049; // Pig Tails
            HairHue = 0x459;
            FacialHairItemID = 0x2041; // Mustache
            FacialHairHue = 0x459;
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());
            AddItem(new ThighBoots(0x901));
            AddItem(new ShortPants(0x730));
            AddItem(new Shirt(0x1BB));
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
