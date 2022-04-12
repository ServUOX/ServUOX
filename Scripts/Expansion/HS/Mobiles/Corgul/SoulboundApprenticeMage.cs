using Server;
using System;

namespace Server.Mobiles
{
    public class SoulboundApprenticeMage : EvilMage
    {
        [Constructible]
        public SoulboundApprenticeMage()
        {
            Title = "the soulbound apprentice mage";

            SetStr(115);
            SetDex(97);
            SetInt(106);

            SetHits(128);
            SetMana(210);

            SetDamage(5, 10);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 20);
            SetResist(ResistType.Fire, 21);
            SetResist(ResistType.Cold, 22);
            SetResist(ResistType.Pois, 20);
            SetResist(ResistType.Engy, 25);

            SetSkill(SkillName.Wrestling, 40.0, 50.0);
            SetSkill(SkillName.MagicResist, 40.0, 50.0);
            SetSkill(SkillName.Magery, 60.2, 72.4);
            SetSkill(SkillName.EvalInt, 60.1, 73.4);
            SetSkill(SkillName.Meditation, 40.0, 50.0);

            Fame = 1000;
            Karma = -1000;
        }


        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 3);
        }

        public override bool OnBeforeDeath()
        {
            if (Region.IsPartOf<Server.Regions.CorgulRegion>())
            {
                CorgulTheSoulBinder.CheckDropSOT(this);
            }

            return base.OnBeforeDeath();
        }

        public SoulboundApprenticeMage(Serial serial)
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
