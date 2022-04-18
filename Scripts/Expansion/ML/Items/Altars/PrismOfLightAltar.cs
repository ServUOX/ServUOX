using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Items
{
    public class PrismOfLightAltar : PeerlessAltar
    {
        private int m_ID;
        public override int KeyCount => 3;
        public override MasterKey MasterKey => new PrismOfLightKey();
        public List<Item> Pedestals = new List<Item>();

        public override Type[] Keys => new Type[]
                {
                    typeof(JaggedCrystals), typeof(BrokenCrystals), typeof(PiecesOfCrystal),
                    typeof(CrushedCrystals), typeof(ScatteredCrystals), typeof(ShatteredCrystals)
                };

        public override BasePeerless Boss => new ShimmeringEffusion();

        [Constructible]
        public PrismOfLightAltar() : base(0x2206)
        {
            Visible = false;

            BossLocation = new Point3D(6520, 122, -20);
            TeleportDest = new Point3D(6520, 139, -20);
            ExitDest = new Point3D(3785, 1107, 20);

            m_ID = 0;
        }

        public override void ClearContainer()
        {
            base.ClearContainer();

            Pedestals.ForEach(x => x.Hue = ((PrismOfLightPillar)x).OrgHue);
        }

        public override Rectangle2D[] BossBounds => m_Bounds;

        private readonly Rectangle2D[] m_Bounds = new Rectangle2D[]
        {
            new Rectangle2D(6500, 111, 45, 35),
        };

        public PrismOfLightAltar(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);

            writer.Write(Pedestals, true);

            writer.Write(m_ID);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        Pedestals = reader.ReadStrongItemList();
                        goto case 0;
                    }
                case 0:
                    {
                        m_ID = reader.ReadInt();
                        break;
                    }

                default:
                    break;
            }
        }

        public int GetID()
        {
            int id = m_ID;
            m_ID += 1;
            return id;
        }
    }
}
