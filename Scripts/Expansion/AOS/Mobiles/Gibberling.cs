using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gibberling corpse")]
    public class Gibberling : BaseCreature
    {
        [Constructible]
        public Gibberling()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a gibberling";
            Body = 307;
            BaseSoundID = 422;

            SetStr(141, 165);
            SetDex(101, 125);
            SetInt(56, 80);

            SetHits(85, 99);

            SetDamage(12, 17);

            SetDamageType(ResistanceType.Physical, 0);
            SetDamageType(ResistanceType.Fire, 40);
            SetDamageType(ResistanceType.Energy, 60);

            SetResist(ResistanceType.Physical, 45, 55);
            SetResist(ResistanceType.Fire, 25, 35);
            SetResist(ResistanceType.Cold, 25, 35);
            SetResist(ResistanceType.Poison, 10, 20);
            SetResist(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 45.1, 70.0);
            SetSkill(SkillName.Tactics, 67.6, 92.5);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 27;

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public Gibberling(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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
