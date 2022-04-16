using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a flesh golem corpse")]
    public class FleshGolem : BaseCreature
    {
        [Constructible]
        public FleshGolem()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a flesh golem";
            Body = 304;
            BaseSoundID = 684;

            SetStr(176, 200);
            SetDex(51, 75);
            SetInt(46, 70);

            SetHits(106, 120);

            SetDamage(ResistType.Phys, 100, 0, 18, 22);

            SetResist(ResistType.Phys, 50, 60);
            SetResist(ResistType.Fire, 25, 35);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 60, 70);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.Tactics, 55.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 70.0);

            Fame = 1000;
            Karma = -1800;

            VirtualArmor = 34;

            SetWeaponAbility(WeaponAbility.BleedAttack);
        }

        public FleshGolem(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
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