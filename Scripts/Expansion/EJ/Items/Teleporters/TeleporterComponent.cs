using Server.ContextMenus;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Items
{
    public class TeleporterComponent : AddonComponent
    {
        public override bool ForceShowProperties => true;

        public Direction _Direction { get; set; }

        public TeleporterComponent(int itemID, Direction d)
            : base(itemID)
        {
            _Direction = d;
        }

        public TeleporterComponent(Serial serial)
            : base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            SetSecureLevelEntry.AddTo(from, Addon, list);
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m is PlayerMobile && ((SpiralStaircaseAddon)Addon).CheckAccessible(m, this))
            {
                Timer.DelayCall(TimeSpan.FromSeconds(0.5), () => DoTeleport(m));
            }

            return base.OnMoveOver(m);
        }

        public virtual void DoTeleport(Mobile m)
        {
            Map map = Map;

            if (map == null || map == Map.Internal)
            {
                map = m.Map;
            }

            TeleporterComponent c = Addon.Components.FirstOrDefault(x => x is TeleporterComponent && x != this) as TeleporterComponent;

            Point3D p = new Point3D(c.Location.X, c.Location.Y, c._Direction == Direction.Up ? Location.Z + 20 : c.Location.Z);

            BaseCreature.TeleportPets(m, p, map);

            m.MoveToWorld(p, map);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write((int)_Direction);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            _Direction = (Direction)reader.ReadInt();
        }
    }
}
