using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Brigand Cannibal corpse")]
    public class BrigandCannibal : Brigand
    {
        [Constructible]
        public BrigandCannibal()
            : base()
        {
            Title = "the brigand cannibal";
            Hue = 33782;

            SetStr(68, 95);
            SetDex(81, 95);
            SetInt(110, 115);

            SetHits(2058, 2126);
            SetMana(552, 553);

            SetDamage(10, 23);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 65, 68);
            SetResist(ResistType.Fire, 65, 66);
            SetResist(ResistType.Cold, 62, 69);
            SetResist(ResistType.Pois, 62, 67);
            SetResist(ResistType.Engy, 64, 68);

            SetSkill(SkillName.MagicResist, 96.9, 96.9);
            SetSkill(SkillName.Tactics, 94.0, 94.0);
            SetSkill(SkillName.Swords, 54.3, 54.3);

            Fame = 14500;
            Karma = -14500;

            VirtualArmor = 16;
        }

        public BrigandCannibal(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
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