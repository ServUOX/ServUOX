using System;

using Server.Mobiles;
using Server.Items;

namespace Server.Engines.SorcerersDungeon
{
    [CorpseName("the corpse of a headless elf")]
    public class HeadlessElf : BaseCreature
    {
        [Constructible]
        public HeadlessElf()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a headless elf";
            Race = Race.Elf;
            Body = 31;

            Hue = Race.RandomSkinHue();
            BaseSoundID = 0x39D;

            SetStr(700, 800);
            SetDex(90, 100);
            SetInt(450, 500);

            SetHits(8000);

            SetDamage(21, 27);

            SetDamageType(ResistType.Phys, 60);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 70, 80);
            SetResist(ResistType.Fire, 70, 80);
            SetResist(ResistType.Cold, 60, 70);
            SetResist(ResistType.Pois, 60, 70);
            SetResist(ResistType.Engy, 60, 70);

            SetSkill(SkillName.Anatomy, 130, 140);
            SetSkill(SkillName.Poisoning, 120);
            SetSkill(SkillName.MagicResist, 130, 140);
            SetSkill(SkillName.Tactics, 130, 140);
            SetSkill(SkillName.Wrestling, 120, 130);
            SetSkill(SkillName.Parry, 20, 30);

            SetSkill(SkillName.Magery, 110, 120);
            SetSkill(SkillName.EvalInt, 110, 120);
            SetSkill(SkillName.Meditation, 110, 120);
            SetSkill(SkillName.Focus, 120, 130);

            Fame = 12000;
            Karma = -12000;

            SetMagicalAbility(MagicalAbility.WrestlingMastery);
        }

        public HeadlessElf(Serial serial)
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
