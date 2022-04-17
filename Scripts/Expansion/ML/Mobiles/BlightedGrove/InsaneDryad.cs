using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an insane dryad corpse")]
    public class InsaneDryad : Dryad
    {
        public override bool InitialInnocent => false;

        [Constructible]
        public InsaneDryad()
            : base()
        {
            Name = "an insane dryad";
            Hue = 0x487;

            FightMode = FightMode.Closest;

            Fame = 7000;
            Karma = -7000;
        }

        public InsaneDryad(Serial serial)
            : base(serial)
        {
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new ParrotItem());
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
