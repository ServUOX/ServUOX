using Server.Engines.VeteranRewards;
using Server.Multis;
using Server.Network;
using System;

namespace Server.Items
{
    public class TreeStump : BaseAddon, IRewardItem
    {
        public override bool ForceShowProperties => true;

        private bool m_IsRewardItem;
        private int m_Logs;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextResourceCount { get; set; }

        [Constructible]
        public TreeStump(int itemID)
            : base()
        {
            AddComponent(new InternalAddonComponent(itemID), 0, 0, 0);
            NextResourceCount = DateTime.UtcNow + TimeSpan.FromDays(1);
        }

        public TreeStump(Serial serial)
            : base(serial)
        {
        }

        public override BaseAddonDeed Deed
        {
            get
            {
                TreeStumpDeed deed = new TreeStumpDeed
                {
                    IsRewardItem = m_IsRewardItem,
                    Logs = m_Logs
                };

                return deed;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get => m_IsRewardItem;
            set
            {
                m_IsRewardItem = value;
                UpdateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Logs
        {
            get => m_Logs;
            set
            {
                m_Logs = value;
                UpdateProperties();
            }
        }
        public override void OnComponentUsed(AddonComponent c, Mobile from)
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            /*
            * Unique problems have unique solutions.  OSI does not have a problem with 1000s of mining carts
            * due to the fact that they have only a miniscule fraction of the number of 10 year vets that a
            * typical ServUOX shard will have (ServUOX's scaled down account aging system makes this a unique problem),
            * and the "freeness" of free accounts. We also dont have mitigating factors like inactive (unpaid)
            * accounts not gaining veteran time.
            *
            * The lack of high end vets and vet rewards on OSI has made testing the *exact* ranging/stacking
            * behavior of these things all but impossible, so either way its just an estimation.
            *
            * If youd like your shard's carts/stumps to work the way they did before, simply replace the check
            * below with this line of code:
            *
            * if (!from.InRange(GetWorldLocation(), 2)
            *
            * However, I am sure these checks are more accurate to OSI than the former version was.
            *
            */

            if (!from.InRange(GetWorldLocation(), 2) || !from.InLOS(this) || !((from.Z - Z) > -3 && (from.Z - Z) < 3))
            {
                from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
            else if (house != null && house.HasSecureAccess(from, SecureLevel.Friends))
            {
                if (m_Logs > 0)
                {
                    Item logs = null;

                    switch (Utility.Random(7))
                    {
                        case 0:
                            logs = new Log();
                            break;
                        case 1:
                            logs = new AshLog();
                            break;
                        case 2:
                            logs = new OakLog();
                            break;
                        case 3:
                            logs = new YewLog();
                            break;
                        case 4:
                            logs = new HeartwoodLog();
                            break;
                        case 5:
                            logs = new BloodwoodLog();
                            break;
                        case 6:
                            logs = new FrostwoodLog();
                            break;
                    }

                    if (logs != null)
                    {
                        int amount = Math.Min(10, m_Logs);
                        logs.Amount = amount;

                        if (!from.PlaceInBackpack(logs))
                        {
                            logs.Delete();
                            from.SendLocalizedMessage(1078837); // Your backpack is full! Please make room and try again.
                        }
                        else
                        {
                            m_Logs -= amount;
                            PublicOverheadMessage(MessageType.Regular, 0, 1094719, m_Logs.ToString()); // Logs: ~1_COUNT~
                        }
                    }
                }
                else
                {
                    from.SendLocalizedMessage(1094720); // There are no more logs available.
                }
            }
            else
            {
                from.SendLocalizedMessage(1061637); // You are not allowed to access 
            }
        }

        private class InternalAddonComponent : AddonComponent
        {
            public InternalAddonComponent(int id)
                : base(id)
            {
            }

            public override void GetProperties(ObjectPropertyList list)
            {
                base.GetProperties(list);

                if (Addon is TreeStump)
                {
                    list.Add(1094719, ((TreeStump)Addon).Logs.ToString()); // Logs: ~1_COUNT~
                }
            }

            public InternalAddonComponent(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.WriteEncodedInt(0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                _ = reader.ReadEncodedInt();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0);

            TryGiveResourceCount();

            writer.Write(m_IsRewardItem);
            writer.Write(m_Logs);
            writer.Write(NextResourceCount);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadEncodedInt();

            m_IsRewardItem = reader.ReadBool();
            m_Logs = reader.ReadInt();
            NextResourceCount = reader.ReadDateTime();
        }

        private void TryGiveResourceCount()
        {
            if (NextResourceCount < DateTime.UtcNow)
            {
                Logs = Math.Min(100, m_Logs + 10);
                NextResourceCount = DateTime.UtcNow + TimeSpan.FromDays(1);
            }
        }
    }
}
