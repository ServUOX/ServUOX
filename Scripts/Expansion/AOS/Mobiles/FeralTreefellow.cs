using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a treefellow corpse")]
    public class FeralTreefellow : BaseCreature
    {
        [Constructible]
        public FeralTreefellow()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a feral treefellow";
            Body = 301;

            SetStr(1351, 1600);
            SetDex(301, 550);
            SetInt(651, 900);

            SetHits(1170, 1320);

            SetDamage(26, 35);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 60, 70);
            SetResist(ResistanceType.Cold, 70, 80);
            SetResist(ResistanceType.Poison, 60, 70);
            SetResist(ResistanceType.Energy, 40, 60);

            SetSkill(SkillName.MagicResist, 40.1, 55.0);// Unknown
            SetSkill(SkillName.Tactics, 65.1, 90.0);// Unknown
            SetSkill(SkillName.Wrestling, 65.1, 85.0);// Unknown

            Fame = 1000;  //Unknown
            Karma = -3000;  //Unknown

            VirtualArmor = 24;
            PackItem(new Log(Utility.RandomMinMax(23, 34)));

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public FeralTreefellow(Serial serial)
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
            AddLoot(LootPack.Average); //Unknown
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
