using Server.Ethics;

namespace Server.Mobiles
{
    [CorpseName("an unholy corpse")]
    public class UnholySteed : BaseMount
    {
        [Constructible]
        public UnholySteed()
            : base("a dark steed", 0x74, 0x3EA7, AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            SetStr(496, 525);
            SetDex(86, 105);
            SetInt(86, 125);

            SetHits(298, 315);

            SetDamage(16, 22);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Fire, 40);
            SetDamageType(ResistanceType.Energy, 20);

            SetResist(ResistanceType.Physical, 55, 65);
            SetResist(ResistanceType.Fire, 30, 40);
            SetResist(ResistanceType.Cold, 30, 40);
            SetResist(ResistanceType.Poison, 30, 40);
            SetResist(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 80.5, 92.5);

            Fame = 14000;
            Karma = -14000;

            VirtualArmor = 60;

            Tamable = false;
            ControlSlots = 1;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public UnholySteed(Serial serial)
            : base(serial)
        {
        }

        public override bool IsDispellable => false;
        public override bool IsBondable => false;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

        public override string ApplyNameSuffix(string suffix)
        {
            if (suffix.Length == 0)
                suffix = Ethic.Evil.Definition.Adjunct.String;
            else
                suffix = string.Concat(suffix, " ", Ethic.Evil.Definition.Adjunct.String);

            return base.ApplyNameSuffix(suffix);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Ethic.Find(from) != Ethic.Evil)
                from.SendMessage("You may not ride this steed.");
            else
                base.OnDoubleClick(from);
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
