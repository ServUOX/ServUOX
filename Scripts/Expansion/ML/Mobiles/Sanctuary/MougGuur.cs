using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Moug-Guur corpse")]
    public class MougGuur : Ettin
    {
        [Constructible]
        public MougGuur()
        {
            Name = "Moug-Guur";

            SetStr(556, 575);
            SetDex(84, 94);
            SetInt(59, 73);

            SetHits(400, 415);

            SetDamage(ResistType.Phys, 100, 0, 12, 20);

            SetResist(ResistType.Phys, 61, 65);
            SetResist(ResistType.Fire, 16, 19);
            SetResist(ResistType.Cold, 41, 46);
            SetResist(ResistType.Pois, 21, 24);
            SetResist(ResistType.Engy, 19, 25);

            SetSkill(SkillName.MagicResist, 70.2, 75.0);
            SetSkill(SkillName.Tactics, 80.8, 81.7);
            SetSkill(SkillName.Wrestling, 93.9, 99.4);

            Fame = 3000;
            Karma = -3000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }
        }

        public MougGuur(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;
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
