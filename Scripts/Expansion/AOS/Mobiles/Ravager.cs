using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a ravager corpse")]
    public class Ravager : BaseCreature
    {
        [Constructible]
        public Ravager()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a ravager";
            Body = 314;
            BaseSoundID = 357;

            SetStr(251, 275);
            SetDex(101, 125);
            SetInt(66, 90);

            SetHits(161, 175);

            SetDamage(15, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 50, 60);
            SetResist(ResistanceType.Fire, 50, 60);
            SetResist(ResistanceType.Cold, 60, 70);
            SetResist(ResistanceType.Poison, 30, 40);
            SetResist(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.Tactics, 75.1, 100.0);
            SetSkill(SkillName.Wrestling, 70.1, 90.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 54;

            SetWeaponAbility(WeaponAbility.CrushingBlow);
            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public Ravager(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 4;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
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
