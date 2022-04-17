using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a crystal sea serpent corpse")]
    public class CrystalSeaSerpent : SeaSerpent
    {
        [Constructible]
        public CrystalSeaSerpent()
        {
            Name = "a crystal sea serpent";
            Hue = 0x47E;

            SetStr(250, 450);
            SetDex(100, 150);
            SetInt(90, 190);

            SetHits(230, 330);

            SetDamage(ResistType.Phys, 10, 0, 10, 18);
            SetDamage(ResistType.Cold, 45);
            SetDamage(ResistType.Engy, 45);

            SetResist(ResistType.Phys, 50, 70);
            SetResist(ResistType.Fire, 0);
            SetResist(ResistType.Cold, 70, 90);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 60, 80);
        }

        public override void OnDeath(Container c)
        {
            if (Utility.RandomDouble() < 0.05)
                c.DropItem(new CrushedCrystals());

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new IcyHeart());

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new LuckyDagger());

            base.OnDeath(c);
        }

        public override int TreasureMapLevel => 3;
        public override int Meat => 10;
        public override int Hides => 11;
        public override HideType HideType => HideType.Horned;
        public override int Scales => 8;
        public override ScaleType ScaleType => ScaleType.Blue;

        public CrystalSeaSerpent(Serial serial)
            : base(serial)
        {
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
