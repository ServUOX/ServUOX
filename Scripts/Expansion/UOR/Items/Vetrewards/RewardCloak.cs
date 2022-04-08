using System;
using Server.Engines.VeteranRewards;
using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable]
    public class RewardCloak : BaseCloak, IRewardItem
    {
        private int m_LabelNumber;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Number
        {
            get
            {
                return m_LabelNumber;
            }
            set
            {
                m_LabelNumber = value;
                InvalidateProperties();
            }
        }

        public override int LabelNumber
        {
            get
            {
                if (m_LabelNumber > 0)
                    return m_LabelNumber;

                return base.LabelNumber;
            }
        }

        public override int BasePhysicalResistance => 3;

        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile)
                ((Mobile)parent).VirtualArmorMod += 2;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
                ((Mobile)parent).VirtualArmorMod -= 2;
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (Core.ML && IsRewardItem)
                list.Add(RewardSystem.GetRewardYearLabel(this, new object[] { Hue, m_LabelNumber })); // X Year Veteran Reward
        }

        public override bool CanEquip(Mobile m)
        {
            if (!base.CanEquip(m))
                return false;

            return !IsRewardItem || RewardSystem.CheckIsUsableBy(m, this, new object[] { Hue, m_LabelNumber });
        }

        [Constructible]
        public RewardCloak()
            : this(0)
        {
        }

        [Constructible]
        public RewardCloak(int hue)
            : this(hue, 0)
        {
        }

        [Constructible]
        public RewardCloak(int hue, int labelNumber)
            : base(0x1515, hue)
        {
            Weight = 5.0;
            LootType = LootType.Blessed;

            m_LabelNumber = labelNumber;
        }

        public RewardCloak(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write(m_LabelNumber);
            writer.Write(IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_LabelNumber = reader.ReadInt();
                        IsRewardItem = reader.ReadBool();
                        break;
                    }
            }

            if (Parent is Mobile)
                ((Mobile)Parent).VirtualArmorMod += 2;
        }
    }
}
