using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a treefellow guardian corpse")]
    public class TreefellowGuardian : BaseCreature
    {
        [Constructible]
        public TreefellowGuardian()
            : base(AIType.AI_Mystic, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            Name = "a Treefellow Guardian";
            Body = 301;

            SetStr(511, 695);
            SetDex(30, 55);
            SetInt(403, 491);

            SetHits(724, 900);

            SetDamage(12, 16);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 30, 35);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Poison, 20, 30);
            SetResist(ResistType.Energy, 80, 90);

            SetSkill(SkillName.MagicResist, 40.1, 55.0);
            SetSkill(SkillName.Tactics, 65.1, 90.0);
            SetSkill(SkillName.Wrestling, 65.1, 85.0);
            SetSkill(SkillName.Spellweaving, 120.0);

            Fame = 500;
            Karma = 1500;

            VirtualArmor = 24;
            PackItem(new Log(Utility.RandomMinMax(23, 34)));

            if (0.05 > Utility.RandomDouble())
                PackItem(new TreefellowWood());

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public TreefellowGuardian(Serial serial)
            : base(serial)
        {
        }

        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override bool BleedImmunity => true;

        public override int GetIdleSound()
        {
            return 443;
        }

        public override int GetDeathSound()
        {
            return 31;
        }

        public override int GetAttackSound()
        {
            return 672;
        }

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
            int version = reader.ReadInt();
        }
    }
}