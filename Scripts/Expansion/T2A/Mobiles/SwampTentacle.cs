using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a swamp tentacle corpse")]
    public class SwampTentacle : BaseCreature
    {
        [Constructible]
        public SwampTentacle()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a swamp tentacle";
            Body = 66;
            BaseSoundID = 352;

            SetStr(96, 120);
            SetDex(66, 85);
            SetInt(16, 30);

            SetHits(58, 72);
            SetMana(0);

            SetDamage(6, 12);

            SetDamageType(ResistType.Physical, 40);
            SetDamageType(ResistType.Poison, 60);

            SetResist(ResistType.Physical, 25, 35);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Poison, 60, 80);
            SetResist(ResistType.Energy, 10, 20);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 65.1, 80.0);
            SetSkill(SkillName.Wrestling, 65.1, 80.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 28;
        }

        public SwampTentacle(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Greater;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            PackItem(Loot.PackReg(3));
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
