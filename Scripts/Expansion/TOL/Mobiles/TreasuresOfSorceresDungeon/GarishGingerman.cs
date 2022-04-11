using System;

using Server.Mobiles;

namespace Server.Engines.SorcerersDungeon
{
    [CorpseName("a garish gingerman corpse")]
    public class GarishGingerman : BaseCreature
    {
        [Constructible]
        public GarishGingerman()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a garish gingerman";

            Body = 14;
            BaseSoundID = 268;
            Hue = 1461;

            SetStr(400);
            SetDex(150);
            SetInt(1200);

            SetHits(8000);

            SetDamage(21, 27);

            SetDamageType(ResistType.Physical, 50);
            SetDamageType(ResistType.Energy, 50);

            SetResist(ResistType.Physical, 60, 70);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 80, 90);
            SetResist(ResistType.Poison, 60, 70);
            SetResist(ResistType.Energy, 100);

            SetSkill(SkillName.EvalInt, 120);
            SetSkill(SkillName.Magery, 120);
            SetSkill(SkillName.Meditation, 100);
            SetSkill(SkillName.MagicResist, 200);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 120);

            Fame = 12000;
            Karma = -12000;

            SetMagicalAbility(MagicalAbility.MageryMastery);
        }

        public GarishGingerman(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;
        public override Poison PoisonImmunity => Poison.Deadly;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
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
