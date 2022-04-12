using Server;
using System;

namespace Server.Mobiles
{
    public class SoulboundBattleMage : EvilMageLord
    {
        [Constructible]
        public SoulboundBattleMage()
        {
            Title = "the soulbound battle mage";

            SetStr(156);
            SetDex(101);
            SetInt(181);

            SetHits(419);
            SetMana(619);

            SetDamage(12, 17);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Cold, 20);
            SetDamageType(ResistType.Pois, 20);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 50, 60);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.Wrestling, 120.0, 125.0);
            SetSkill(SkillName.Tactics, 110.0, 120.0);
            SetSkill(SkillName.MagicResist, 100.0, 110.0);
            SetSkill(SkillName.Anatomy, 1.0, 0.0);
            SetSkill(SkillName.Magery, 105.0, 110.0);
            SetSkill(SkillName.EvalInt, 95.0, 100.0);
            SetSkill(SkillName.Meditation, 20.0, 30.0);

            Fame = 5000;
            Karma = -5000;
        }


        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
        }

        public SoulboundBattleMage(Serial serial)
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