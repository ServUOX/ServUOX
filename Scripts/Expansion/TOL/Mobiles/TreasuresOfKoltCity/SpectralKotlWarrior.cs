using System;
using Server;
using Server.Items;
using Server.Engines.MyrmidexInvasion;

namespace Server.Mobiles
{
    [CorpseName("a kotl warrior corpse")]
    public class SpectralKotlWarrior : BaseCreature
    {
        [Constructible]
        public SpectralKotlWarrior(bool weak)
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "a spectral kotl warrior";

            Body = 0x24;
            BaseSoundID = 417;
            Hue = 2951;

            SetStr(500, 600);
            SetDex(82, 95);
            SetInt(130, 140);
            SetMana(40, 50);

            SetDamage(18, 22);

            if (weak)
            {
                SetHits(1200, 1400);

                SetResist(ResistType.Phys, 1, 10);
                SetResist(ResistType.Fire, 1, 10);
                SetResist(ResistType.Cold, 1, 10);
                SetResist(ResistType.Pois, 8, 10);
                SetResist(ResistType.Engy, 5, 10);
            }
            else
            {
                SetHits(330, 360);

                SetResist(ResistType.Phys, 40, 50);
                SetResist(ResistType.Fire, 30, 40);
                SetResist(ResistType.Cold, 30, 40);
                SetResist(ResistType.Pois, 90, 100);
                SetResist(ResistType.Engy, 80, 90);
            }

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Pois, 50);

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
            if (m is SpectralMyrmidexWarrior)
                return true;

            return base.IsEnemy(m);
        }

        public SpectralKotlWarrior(Serial serial)
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