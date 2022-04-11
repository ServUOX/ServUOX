using System;
using Server;
using Server.Items;
using Server.Engines.MyrmidexInvasion;

namespace Server.Mobiles
{
    [CorpseName("a myrmidex corpse")]
    public class SpectralMyrmidexWarrior : BaseCreature
    {
        [Constructible]
        public SpectralMyrmidexWarrior(bool weak)
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "a spectral myrmidex warrior";

            Body = 1403;
            BaseSoundID = 959;
            Hue = 2951;

            SetStr(500, 600);
            SetDex(82, 95);
            SetInt(130, 140);
            SetMana(40, 50);

            SetDamage(18, 22);

            if (weak)
            {
                SetHits(1200, 1400);

                SetResist(ResistType.Physical, 1, 10);
                SetResist(ResistType.Fire, 1, 10);
                SetResist(ResistType.Cold, 1, 10);
                SetResist(ResistType.Poison, 8, 10);
                SetResist(ResistType.Energy, 5, 10);
            }
            else
            {
                SetHits(330, 360);

                SetResist(ResistType.Physical, 40, 50);
                SetResist(ResistType.Fire, 30, 40);
                SetResist(ResistType.Cold, 30, 40);
                SetResist(ResistType.Poison, 90, 100);
                SetResist(ResistType.Energy, 80, 90);
            }

            SetDamageType(ResistType.Physical, 50);
            SetDamageType(ResistType.Poison, 50);

            SetSkill(SkillName.Wrestling, 90, 100);
            SetSkill(SkillName.Tactics, 90, 100);
            SetSkill(SkillName.MagicResist, 70, 80);
            SetSkill(SkillName.Poisoning, 70, 80);
            SetSkill(SkillName.Magery, 80, 90);
            SetSkill(SkillName.EvalInt, 70, 80);

            Fame = 16000;
            Karma = -16000;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public override Poison HitPoison => Poison.Deadly;
        public override Poison PoisonImmunity => Poison.Deadly;

        public override bool IsEnemy(Mobile m)
        {
            if (m is SpectralKotlWarrior)
                return true;

            return base.IsEnemy(m);
        }

        public SpectralMyrmidexWarrior(Serial serial)
            : base(serial)
        {
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