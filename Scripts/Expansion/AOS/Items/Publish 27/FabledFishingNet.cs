using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class FabledFishingNet : SpecialFishingNet
    {
        [Constructible]
        public FabledFishingNet()
        {
            Hue = 0x481;
        }

        public FabledFishingNet(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1063451;// a fabled fishing net
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            if (Weight != 15.0)
            {
                Weight = 15.0;
            }
        }

        protected override void AddNetProperties(ObjectPropertyList list)
        {
        }

        protected override int GetSpawnCount()
        {
            return base.GetSpawnCount() + 4;
        }

        protected override void FinishEffect(Point3D p, Map map, Mobile from)
        {
            BaseCreature toSpawn;

            if (Core.HS && 0.125 > Utility.RandomDouble())
            {
                toSpawn = new Osiredon(from);
            }
            else
            {
                toSpawn = new Leviathan(from);
            }

            Spawn(p, map, toSpawn);

            base.FinishEffect(p, map, from);
        }
    }
}
