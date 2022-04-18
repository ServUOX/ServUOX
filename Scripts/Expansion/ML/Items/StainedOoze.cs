using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class StainedOoze : Item
    {
        private Timer m_Timer;
        private int m_Ticks;

        [Constructible]
        public StainedOoze()
            : this(false)
        {
        }

        [Constructible]
        public StainedOoze(bool corrosive)
            : base(0x122A)
        {
            Movable = false;
            Hue = 0x95;

            Corrosive = corrosive;
            m_Timer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(1), OnTick);
            m_Ticks = 0;
        }

        public StainedOoze(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Corrosive { get; set; }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
                m_Timer = null;
            }
        }

        public void Damage(Mobile m)
        {
            if (Corrosive)
            {
                List<Item> items = m.Items;
                bool damaged = false;

                for (int i = 0; i < items.Count; ++i)
                {
                    if (items[i] is IDurability wearable && wearable.HitPoints >= 10 && Utility.RandomDouble() < 0.25)
                    {
                        wearable.HitPoints -= (wearable.HitPoints == 10) ? Utility.Random(1, 5) : 10;
                        damaged = true;
                    }
                }

                if (damaged)
                {
                    m.LocalOverheadMessage(MessageType.Regular, 0x21, 1072070); // The infernal ooze scorches you, setting you and your equipment ablaze!
                    return;
                }
            }

            AOS.Damage(m, 40, 0, 0, 0, 100, 0);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(Corrosive);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            Corrosive = reader.ReadBool();

            m_Timer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(1), OnTick);
            m_Ticks = (ItemID == 0x122A) ? 0 : 30;
        }

        private void OnTick()
        {
            List<Mobile> toDamage = new List<Mobile>();
            IPooledEnumerable eable = GetMobilesInRange(0);

            foreach (Mobile m in eable)
            {
                if (m is BaseCreature bc)
                {
                    if (!bc.Controlled && !bc.Summoned)
                        continue;
                }
                else if (!m.Player)
                {
                    continue;
                }

                if (m.Alive && !m.IsDeadBondedPet && m.CanBeDamaged())
                    toDamage.Add(m);
            }

            eable.Free();

            for (int i = 0; i < toDamage.Count; ++i)
                Damage(toDamage[i]);

            ++m_Ticks;

            if (m_Ticks >= 35)
                Delete();
            else if (m_Ticks == 30)
                ItemID = 0x122B;
        }
    }
}
