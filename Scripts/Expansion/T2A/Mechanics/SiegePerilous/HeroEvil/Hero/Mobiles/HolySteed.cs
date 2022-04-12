using System;
using Server.Ethics;

namespace Server.Mobiles
{
    [CorpseName("a holy corpse")]
    public class HolySteed : BaseMount
    {
        [Constructible]
        public HolySteed()
            : base("a silver steed", 0x75, 0x3EA8, AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            SetStr(496, 525);
            SetDex(86, 105);
            SetInt(86, 125);

            SetHits(298, 315);

            SetDamage(16, 22);

            SetDamageType(ResistType.Phys, 40);
            SetDamageType(ResistType.Fire, 40);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 20, 30);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 80.5, 92.5);

            Fame = 14000;
            Karma = 14000;

            VirtualArmor = 60;

            Tamable = false;
            ControlSlots = 1;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public HolySteed(Serial serial)
            : base(serial)
        {
        }

        public override bool IsDispellable => false;
        public override bool IsBondable => false;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override string ApplyNameSuffix(string suffix)
        {
            if (suffix.Length == 0)
                suffix = Ethic.Hero.Definition.Adjunct.String;
            else
                suffix = string.Concat(suffix, " ", Ethic.Hero.Definition.Adjunct.String);

            return base.ApplyNameSuffix(suffix);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Ethic.Find(from) != Ethic.Hero)
                from.SendMessage("You may not ride this steed.");
            else
                base.OnDoubleClick(from);
        }

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
