using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a treefellow corpse")]
    public class Treefellow : BaseCreature
    {
        [Constructible]
        public Treefellow()
            : base(AIType.AI_Melee, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            Name = "a treefellow";
            Body = 301;

            SetStr(196, 220);
            SetDex(31, 55);
            SetInt(66, 90);

            SetHits(118, 132);

            SetDamage(12, 16);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 20, 25);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Poison, 30, 35);
            SetResist(ResistType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 40.1, 55.0);
            SetSkill(SkillName.Tactics, 65.1, 90.0);
            SetSkill(SkillName.Wrestling, 65.1, 85.0);

            Fame = 500;
            Karma = 1500;

            VirtualArmor = 24;
            PackItem(new Log(Utility.RandomMinMax(23, 34)));

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public Treefellow(Serial serial)
            : base(serial)
        {
        }

        public override TribeType Tribe => TribeType.Fey;

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
