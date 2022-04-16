using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("a dark wisp corpse")]
    public class DarkWisp : BaseCreature
    {
        [Constructible]
        public DarkWisp()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "dark wisp";
            Body = 165;
            BaseSoundID = 466;

            SetStr(196, 225);
            SetDex(196, 225);
            SetInt(196, 225);

            SetHits(118, 135);

            SetDamage(ResistType.Phys, 50, 0, 17, 18);
            SetDamage(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 35, 45);
            SetResist(ResistType.Fire, 20, 40);
            SetResist(ResistType.Cold, 10, 30);
            SetResist(ResistType.Pois, 5, 10);
            SetResist(ResistType.Engy, 50, 70);

            SetSkill(SkillName.EvalInt, 80.0);
            SetSkill(SkillName.Magery, 80.0);
            SetSkill(SkillName.MagicResist, 80.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Wrestling, 80.0);
            SetSkill(SkillName.Necromancy, 80.0);
            SetSkill(SkillName.SpiritSpeak, 80.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 40;

            AddItem(new LightSource());
        }

        public DarkWisp(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Wisp;
        public override TimeSpan ReacquireDelay => TimeSpan.FromSeconds(1.0);
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
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
