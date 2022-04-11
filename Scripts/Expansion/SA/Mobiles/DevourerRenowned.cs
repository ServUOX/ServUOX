using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Devourer of Souls [Renowned] corpse")]
    public class DevourerRenowned : BaseRenowned
    {
        [Constructible]
        public DevourerRenowned()
            : base(AIType.AI_NecroMage)
        {
            Name = "Devourer of Souls";
            Title = "[Renowned]";
            Body = 303;
            BaseSoundID = 357;

            SetStr(801, 950);
            SetDex(126, 175);
            SetInt(201, 250);

            SetHits(2000);

            SetDamage(22, 26);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType(ResistanceType.Energy, 20);

            SetResist(ResistanceType.Physical, 45, 55);
            SetResist(ResistanceType.Fire, 25, 35);
            SetResist(ResistanceType.Cold, 15, 25);
            SetResist(ResistanceType.Poison, 60, 70);
            SetResist(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.Necromancy, 90.1, 100.0);
            SetSkill(SkillName.SpiritSpeak, 90.1, 100.0);
            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 90.1, 105.0);
            SetSkill(SkillName.Tactics, 75.1, 85.0);
            SetSkill(SkillName.Wrestling, 80.1, 100.0);

            Fame = 9500;
            Karma = -9500;

            VirtualArmor = 44;

            PackItem(Loot.PackNecroReg(24, 45));
        }

        public DevourerRenowned(Serial serial)
            : base(serial)
        {
        }

        public override Type[] UniqueSAList => new Type[] { };
        public override Type[] SharedSAList => new Type[] { typeof(AnimatedLegsoftheInsaneTinker), typeof(StormCaller), typeof(PillarOfStrength) };
        public override Poison PoisonImmunity => Poison.Lethal;
        public override int Meat => 3;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
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
