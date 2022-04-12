using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Szavetra corpse")]
    public class Szavetra : Succubus
    {
        [Constructible]
        public Szavetra()
        {
            Name = "Szavetra";

            SetStr(627, 655);
            SetDex(164, 193);
            SetInt(566, 595);

            SetHits(312, 415);

            SetDamage(20, 30);

            SetDamageType(ResistType.Phys, 75);
            SetDamageType(ResistType.Engy, 25);

            SetResist(ResistType.Phys, 83, 90);
            SetResist(ResistType.Fire, 72, 80);
            SetResist(ResistType.Cold, 40, 49);
            SetResist(ResistType.Pois, 51, 60);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.EvalInt, 90.3, 99.8);
            SetSkill(SkillName.Magery, 100.1, 100.6); // 10.1-10.6 on OSI, bug?
            SetSkill(SkillName.Meditation, 90.1, 110.0);
            SetSkill(SkillName.MagicResist, 112.2, 127.2);
            SetSkill(SkillName.Tactics, 91.2, 92.8);
            SetSkill(SkillName.Wrestling, 80.2, 86.4);

            Fame = 24000;
            Karma = -24000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetSpecialAbility(SpecialAbility.LifeDrain);
        }

        public Szavetra(Serial serial)
            : base(serial)
        {
        }

        public override bool CanBeParagon => false;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
