namespace Server.Mobiles
{
    [CorpseName("a maggoty corpse")] // TODO: Corpse name?
    public class MoundOfMaggots : BaseCreature
    {
        [Constructible]
        public MoundOfMaggots()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a mound of maggots";
            Body = 319;
            BaseSoundID = 898;

            SetStr(61, 70);
            SetDex(61, 70);
            SetInt(10);

            SetMana(0);

            SetDamage(3, 9);
            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Pois, 50);

            SetResist(ResistType.Phys, 90);
            SetResist(ResistType.Pois, 100);

            SetSkill(SkillName.Tactics, 50.0);
            SetSkill(SkillName.Wrestling, 50.1, 60.0);

            Fame = 1000;
            Karma = -1000;

            VirtualArmor = 24;
        }

        public MoundOfMaggots(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Gems);
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
