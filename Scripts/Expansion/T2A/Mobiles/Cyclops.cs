namespace Server.Mobiles
{
    [CorpseName("a cyclopean corpse")]
    public class Cyclops : BaseCreature
    {
        [Constructible]
        public Cyclops()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a cyclopean warrior";
            Body = 75;
            BaseSoundID = 604;

            SetStr(336, 385);
            SetDex(96, 115);
            SetInt(31, 55);

            SetHits(202, 231);
            SetMana(0);

            SetDamage(7, 23);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 45, 50);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.MagicResist, 60.3, 105.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 48;
        }

        public Cyclops(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 4;
        public override int TreasureMapLevel => 3;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
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
