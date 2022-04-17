using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a black order master corpse")]
    public class TigersClawMaster : TigersClawThief
    {
        [Constructible]
        public TigersClawMaster()
            : base()
        {
            Name = "Black Order Master";
            Title = "of the Serpent's Fang Sect";
            SetStr(440, 460);
            SetDex(400, 415);
            SetInt(200, 215);

            SetHits(850, 875);

            SetDamage(ResistType.Phys, 100, 0, 15, 20);

            Fame = 25000;
            Karma = -25000;

            VirtualArmor = 60;
        }

        public TigersClawMaster(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;
        public override bool DisplayFameTitle => false;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosFilthyRich, 6);
        }

        public override void OnDeath(Container c)
        {
            c.DropItem(new TigerClawKey());

            if (Utility.RandomDouble() < 0.5)
                c.DropItem(new TigerClawSectBadge());

            base.OnDeath(c);
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
