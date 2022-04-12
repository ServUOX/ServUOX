using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a moloch corpse")]
    public class Moloch : BaseCreature
    {
        [Constructible]
        public Moloch()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a moloch";
            Body = 0x311;
            BaseSoundID = 0x300;

            SetStr(331, 360);
            SetDex(66, 85);
            SetInt(41, 65);

            SetHits(171, 200);

            SetDamage(15, 23);

            SetResist(ResistType.Phys, 60, 70);
            SetResist(ResistType.Fire, 60, 70);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 20, 30);

            SetSkill(SkillName.MagicResist, 65.1, 75.0);
            SetSkill(SkillName.Tactics, 75.1, 90.0);
            SetSkill(SkillName.Wrestling, 70.1, 90.0);

            Fame = 7500;
            Karma = -7500;

            VirtualArmor = 32;

            SetWeaponAbility(WeaponAbility.ConcussionBlow);
        }

        public Moloch(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Regular;

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
