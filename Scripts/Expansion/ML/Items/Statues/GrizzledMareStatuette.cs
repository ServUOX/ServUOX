using System;
using Server.Mobiles;

namespace Server.Items
{
    public class GrizzledMareStatuette : BaseImprisonedMobile
    {
        public override int LabelNumber => 1074475;  // Grizzled Mare Statuette

        [Constructible]
        public GrizzledMareStatuette()
            : base(0x2617)
        {
            Weight = 1.0;
        }

        public GrizzledMareStatuette(Serial serial)
            : base(serial)
        {
        }

        public override BaseCreature Summon => new GrizzledMare();

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

namespace Server.Mobiles
{
    public class GrizzledMare : BaseMount
    {
        [Constructible]
        public GrizzledMare()
            : this("Grizzled Mare")
        {
        }

        [Constructible]
        public GrizzledMare(string name)
            : base(name, 793, 0x3EBB, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            SetStr(100);
            SetDex(80);
            SetInt(100);

            SetHits(100);
            SetMana(0);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Fire, 20);
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType(ResistanceType.Poison, 20);
            SetDamageType(ResistanceType.Energy, 20);

            SetResist(ResistanceType.Physical, 40, 50);
            SetResist(ResistanceType.Fire, 30, 40);
            SetResist(ResistanceType.Cold, 30, 40);
            SetResist(ResistanceType.Poison, 30, 40);
            SetResist(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Anatomy, 0, 5);
            SetSkill(SkillName.MagicResist, 0, 95);
            SetSkill(SkillName.Tactics, 0, 85);
            SetSkill(SkillName.Wrestling, 0, 85);
            SetSkill(SkillName.Necromancy, 18);
            SetSkill(SkillName.SpiritSpeak, 18);

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 30.0;
        }

        public GrizzledMare(Serial serial)
            : base(serial)
        {
        }

        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool DeleteOnRelease => true;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(2); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version < 2)
                reader.ReadInt();
        }
    }
}
