using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class ParoxysmusAltar : PeerlessAltar
    {
        public static Dictionary<Mobile, Timer> ProtectionTable = new Dictionary<Mobile, Timer>();

        public override int KeyCount => 16;
        public override MasterKey MasterKey => new ParoxysmusKey();

        public override Type[] Keys => new Type[]
        {
            typeof( CoagulatedLegs ), typeof( PartiallyDigestedTorso ),
            typeof( GelatanousSkull ), typeof( SpleenOfThePutrefier )
        };

        public override BasePeerless Boss => new ChiefParoxysmus();

        [Constructible]
        public ParoxysmusAltar() : base(0x207A)
        {
            Hue = 0x465;

            BossLocation = new Point3D(6517, 357, 0);
            TeleportDest = new Point3D(6519, 381, 0);
            ExitDest = new Point3D(5623, 3038, 15);
        }

        public override Rectangle2D[] BossBounds => m_Bounds;

        private readonly Rectangle2D[] m_Bounds = new Rectangle2D[]
        {
            new Rectangle2D(6501, 351, 35, 48),
        };

        public static void AddProtection(Mobile m)
        {
            if (ProtectionTable != null && !ProtectionTable.ContainsKey(m))
            {
                ProtectionTable[m] = Timer.DelayCall(TimeSpan.FromMinutes(5), () => Damage(m));
            }
        }

        public static bool IsUnderEffects(Mobile m)
        {
            return ProtectionTable != null && ProtectionTable.ContainsKey(m);
        }

        public static void Damage(Mobile m)
        {

            if (ProtectionTable.TryGetValue(m, out Timer t))
            {
                if (t != null)
                {
                    t.Stop();
                }

                ProtectionTable.Remove(m);
            }
        }

        public ParoxysmusAltar(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1); // version

            writer.Write(ProtectionTable.Count);

            foreach (var kvp in ProtectionTable)
            {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value.Next);
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
                        int count = reader.ReadInt();

                        for (int i = 0; i < count; ++i)
                        {
                            Mobile m = reader.ReadMobile();
                            DateTime end = reader.ReadDateTime();

                            ProtectionTable[m] = Timer.DelayCall(end - DateTime.UtcNow, () => Damage(m));
                        }
                        break;
                    }
                case 0:
                    {
                        break;
                    }

                default:
                    break;
            }

            if (version < 1)
            {
                IPooledEnumerable eable = Map.GetItemsInBounds(new Rectangle2D(6516, 492, 5, 1));

                foreach (Item item in eable)
                {
                    if (item.Movable)
                        item.Delete();
                }

                eable.Free();

                Item gate = new ParoxysmusIronGate(this);
                gate.MoveToWorld(new Point3D(6518, 492, -50), Map);
            }
        }
    }
}
