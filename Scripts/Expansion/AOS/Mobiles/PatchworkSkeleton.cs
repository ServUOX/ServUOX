using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a patchwork skeletal corpse")]
    public class PatchworkSkeleton : BaseCreature
    {
        [Constructible]
        public PatchworkSkeleton()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a patchwork skeleton";
            Body = 309;
            BaseSoundID = 0x48D;

            SetStr(96, 120);
            SetDex(71, 95);
            SetInt(16, 40);

            SetHits(58, 72);

            SetDamage(18, 22);

            SetDamageType(ResistType.Phys, 85);
            SetDamageType(ResistType.Cold, 15);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 70, 80);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.MagicResist, 70.1, 95.0);
            SetSkill(SkillName.Tactics, 55.1, 80.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 500;
            Karma = -500;

            VirtualArmor = 54;

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public PatchworkSkeleton(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
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
