using Server.Ethics;

namespace Server.Mobiles
{
    [CorpseName("an evil corpse")]
    public class UnholyFamiliar : BaseCreature
    {
        [Constructible]
        public UnholyFamiliar()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a dark wolf";
            Body = 99;
            BaseSoundID = 0xE5;

            SetStr(96, 120);
            SetDex(81, 105);
            SetInt(36, 60);

            SetHits(58, 72);
            SetMana(0);

            SetDamage(11, 17);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 20, 25);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 5, 10);
            SetResist(ResistType.Pois, 5, 10);
            SetResist(ResistType.Engy, 10, 15);

            SetSkill(SkillName.MagicResist, 57.6, 75.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 2500;
            Karma = 2500;

            VirtualArmor = 22;

            Tamable = false;
            ControlSlots = 1;
        }

        public UnholyFamiliar(Serial serial)
            : base(serial)
        {
        }

        public override bool IsDispellable => false;
        public override bool IsBondable => false;
        public override int Meat => 1;
        public override int Hides => 7;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Canine;

        public override string ApplyNameSuffix(string suffix)
        {
            if (suffix.Length == 0)
                suffix = Ethic.Evil.Definition.Adjunct.String;
            else
                suffix = string.Concat(suffix, " ", Ethic.Evil.Definition.Adjunct.String);

            return base.ApplyNameSuffix(suffix);
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
