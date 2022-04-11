using System;

using Server.Mobiles;

namespace Server.Engines.SorcerersDungeon
{
    [CorpseName("a stocking serpent corpse")]
    public class StockingSerpent : BaseCreature
    {
        [Constructible]
        public StockingSerpent()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a stocking serpent";

            Body = 0x509;
            BaseSoundID = 0xDB;
            Hue = 1459;

            SetStr(800);
            SetDex(150);
            SetInt(1200);

            SetHits(8000);

            SetDamage(22, 29);

            SetDamageType(ResistType.Physical, 50);
            SetDamageType(ResistType.Cold, 50);

            SetResist(ResistType.Physical, 60, 70);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 100);
            SetResist(ResistType.Poison, 60, 70);
            SetResist(ResistType.Energy, 60, 70);

            SetSkill(SkillName.Anatomy, 115, 120);
            SetSkill(SkillName.Poisoning, 120);
            SetSkill(SkillName.MagicResist, 115, 120);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 120, 130);

            Fame = 12000;
            Karma = -12000;

            SetSpecialAbility(SpecialAbility.ViciousBite);
        }

        public StockingSerpent(Serial serial)
            : base(serial)
        {
        }

        public override Poison HitPoison => Poison.Lethal;
        public override bool AlwaysMurderer => true;
        public override Poison PoisonImmunity => Poison.Lethal;

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
