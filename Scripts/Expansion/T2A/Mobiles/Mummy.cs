using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a mummy corpse")]
    public class Mummy : BaseCreature
    {
        [Constructible]
        public Mummy()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8)
        {
            Name = "a mummy";
            Body = 154;
            BaseSoundID = 471;

            SetStr(346, 370);
            SetDex(71, 90);
            SetInt(26, 40);

            SetHits(208, 222);

            SetDamage(13, 23);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Cold, 60);

            SetResist(ResistanceType.Physical, 45, 55);
            SetResist(ResistanceType.Fire, 10, 20);
            SetResist(ResistanceType.Cold, 50, 60);
            SetResist(ResistanceType.Poison, 20, 30);
            SetResist(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 15.1, 40.0);
            SetSkill(SkillName.Tactics, 35.1, 50.0);
            SetSkill(SkillName.Wrestling, 35.1, 50.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 50;
        }

        public Mummy(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 1;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lesser;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems);
            AddLoot(LootPack.Potions);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.ML && Utility.RandomDouble() < .33)
                PackItem(Engines.Plants.Seed.RandomPeculiarSeed(3));

            PackItem(new Garlic(5));
            PackItem(new Bandage(10));

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
