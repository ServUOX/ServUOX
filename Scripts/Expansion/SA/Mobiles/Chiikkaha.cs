using System;

namespace Server.Mobiles
{
    [CorpseName("a Chiikkaha the Toothed corpse")]
    public class Chiikkaha : RatmanMage
    {
        [Constructible]
        public Chiikkaha()
        {
            Name = "Chiikkaha the Toothed";

            SetStr(450, 476);
            SetDex(157, 179);
            SetInt(251, 275);

            SetHits(400, 425);

            SetDamage(10, 17);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 40, 45);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Poison, 10, 20);
            SetResist(ResistType.Energy, 100);

            SetSkill(SkillName.EvalInt, 70.1, 80.0);
            SetSkill(SkillName.Magery, 70.1, 90.0);
            SetSkill(SkillName.MagicResist, 65.1, 96.0);
            SetSkill(SkillName.Tactics, 50.1, 75.0);
            SetSkill(SkillName.Wrestling, 50.1, 75.0);

            Fame = 7500;
            Karma = -7500;
        }

        public Chiikkaha(Serial serial)
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
