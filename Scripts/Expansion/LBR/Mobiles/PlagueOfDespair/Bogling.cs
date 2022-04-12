using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a plant corpse")]
    public class Bogling : BaseCreature
    {
        [Constructible]
        public Bogling()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bogling";
            Body = 779;
            BaseSoundID = 422;

            SetStr(96, 120);
            SetDex(91, 115);
            SetInt(21, 45);

            SetHits(58, 72);

            SetDamage(5, 7);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 20, 25);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 15, 25);
            SetResist(ResistType.Engy, 15, 25);

            SetSkill(SkillName.MagicResist, 75.1, 100.0);
            SetSkill(SkillName.Tactics, 55.1, 80.0);
            SetSkill(SkillName.Wrestling, 55.1, 75.0);

            Fame = 450;
            Karma = -450;

            VirtualArmor = 28;
        }

        public Bogling(Serial serial)
            : base(serial)
        {
        }

        public override int Hides => 6;
        public override int Meat => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new Log(4));
            CorpseLoot.DropItem(new Engines.Plants.Seed());

            base.OnDeath(CorpseLoot);
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
