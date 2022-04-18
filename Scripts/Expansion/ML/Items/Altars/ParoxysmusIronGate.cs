namespace Server.Items
{
    public class ParoxysmusIronGate : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public PeerlessAltar Altar { get; set; }

        [Constructible]
        public ParoxysmusIronGate()
            : this(null)
        {
        }

        [Constructible]
        public ParoxysmusIronGate(PeerlessAltar altar)
            : base(0x857)
        {
            Altar = altar;
            Movable = false;
        }

        public ParoxysmusIronGate(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClickDead(Mobile from)
        {
            base.OnDoubleClickDead(from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Alive)
                return;

            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(Network.MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
            else if (Altar != null && Altar.Fighters.Contains(from))
            {
                from.SendLocalizedMessage(1075070); // The rusty gate cracks open as you step through...
                from.MoveToWorld(Altar.TeleportDest, Altar.Map);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(Altar);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            Altar = reader.ReadItem() as PeerlessAltar;
        }
    }
}
