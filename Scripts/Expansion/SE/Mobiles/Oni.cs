using System;

namespace Server.Mobiles
{
    [CorpseName("an oni corpse")]
    public class Oni : BaseCreature
    {
        //private DateTime m_NextAbilityTime;

        [Constructible]
        public Oni()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an oni";
            Body = 241;

            SetStr(801, 910);
            SetDex(151, 300);
            SetInt(171, 195);

            SetHits(401, 530);

            SetDamage(14, 20);

            SetDamageType(ResistType.Phys, 70);
            SetDamageType(ResistType.Fire, 10);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 65, 80);
            SetResist(ResistType.Fire, 50, 70);
            SetResist(ResistType.Cold, 35, 50);
            SetResist(ResistType.Pois, 45, 70);
            SetResist(ResistType.Engy, 45, 65);

            SetSkill(SkillName.EvalInt, 100.1, 125.0);
            SetSkill(SkillName.Magery, 96.1, 106.0);
            SetSkill(SkillName.Anatomy, 85.1, 95.0);
            SetSkill(SkillName.MagicResist, 85.1, 100.0);
            SetSkill(SkillName.Tactics, 86.1, 101.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 15000;
            Karma = -15000;

            if (Utility.RandomDouble() < .33)
                PackItem(Engines.Plants.Seed.RandomBonsaiSeed());

            SetSpecialAbility(SpecialAbility.AngryFire);
        }

        public Oni(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 4;
        public override int GetAngerSound()
        {
            return 0x4E3;
        }

        public override int GetIdleSound()
        {
            return 0x4E2;
        }

        public override int GetAttackSound()
        {
            return 0x4E1;
        }

        public override int GetHurtSound()
        {
            return 0x4E4;
        }

        public override int GetDeathSound()
        {
            return 0x4E0;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
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
