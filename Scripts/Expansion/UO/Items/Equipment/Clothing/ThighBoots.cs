using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable]
    public class ThighBoots : BaseShoes, IArcaneEquip
    {
        #region Arcane Impl
        private int m_MaxArcaneCharges, m_CurArcaneCharges;

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxArcaneCharges
        {
            get => m_MaxArcaneCharges;
            set
            {
                m_MaxArcaneCharges = value;
                InvalidateProperties();
                Update();
            }
        }
        public int TempHue { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public int CurArcaneCharges
        {
            get => m_CurArcaneCharges;
            set
            {
                m_CurArcaneCharges = value;
                InvalidateProperties();
                Update();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsArcane => m_MaxArcaneCharges > 0 && m_CurArcaneCharges >= 0;

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);

            if (IsArcane)
            {
                LabelTo(from, 1061837, $"{m_CurArcaneCharges}\t{m_MaxArcaneCharges}");
            }
        }

        public void Update()
        {
            if (IsArcane)
            {
                ItemID = 0x26AF;
            }
            else if (ItemID == 0x26AF)
            {
                ItemID = 0x1711;
            }

            if (IsArcane && CurArcaneCharges == 0)
            {
                TempHue = Hue;
                Hue = 0;
            }
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            base.AddCraftedProperties(list);

            if (IsArcane)
            {
                list.Add(1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges); // arcane charges: ~1_val~ / ~2_val~
            }
        }

        public void Flip()
        {
            if (ItemID == 0x1711)
            {
                ItemID = 0x1712;
            }
            else if (ItemID == 0x1712)
            {
                ItemID = 0x1711;
            }
        }

        #endregion

        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        [Constructible]
        public ThighBoots()
            : this(0)
        {
        }

        [Constructible]
        public ThighBoots(int hue)
            : base(0x1711, hue)
        {
            Weight = 4.0;
        }

        public ThighBoots(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            if (IsArcane)
            {
                writer.Write(true);
                writer.Write(m_CurArcaneCharges);
                writer.Write(m_MaxArcaneCharges);
            }
            else
            {
                writer.Write(false);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        if (reader.ReadBool())
                        {
                            m_CurArcaneCharges = reader.ReadInt();
                            m_MaxArcaneCharges = reader.ReadInt();

                            if (Hue == 2118)
                            {
                                Hue = ArcaneGem.DefaultArcaneHue;
                            }
                        }

                        break;
                    }
            }
        }
    }
}
