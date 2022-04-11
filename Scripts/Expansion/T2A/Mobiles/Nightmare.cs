using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a nightmare corpse")]
    public class Nightmare : BaseMount
    {
        [Constructible]
        public Nightmare()
            : this("a nightmare")
        {
        }

        [Constructible]
        public Nightmare(string name)
            : base(name, 0x74, 0x3EA7, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = Core.AOS ? 0xA8 : 0x16A;

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

            SetSkill(SkillName.EvalInt, 10.4, 50.0);
            SetSkill(SkillName.Magery, 10.4, 50.0);
            SetSkill(SkillName.MagicResist, 85.3, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 80.5, 92.5);

            Fame = 14000;
            Karma = -14000;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 95.1;

            switch (Utility.Random(4))
            {
                case 0:
                    {
                        BodyValue = 116;
                        ItemID = 16039;
                        break;
                    }
                case 1:
                    {
                        BodyValue = 177;
                        ItemID = 16053;
                        break;
                    }
                case 2:
                    {
                        BodyValue = 178;
                        ItemID = 16041;
                        break;
                    }
                case 3:
                    {
                        BodyValue = 179;
                        ItemID = 16055;
                        break;
                    }
            }

            if (Utility.RandomDouble() < 0.05)
                Hue = 1910;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Nightmare(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 5;
        public override int Hides => 10;
        public override HideType HideType => HideType.Barbed;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAngerOnTame => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Potions);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            PackItem(new SulfurousAsh(Utility.RandomMinMax(3, 5)));

            switch (Utility.Random(12))
            {
                case 0: PackItem(new BloodOathScroll()); break;
                case 1: PackItem(new HorrificBeastScroll()); break;
                case 2: PackItem(new StrangleScroll()); break;
                case 3: PackItem(new VengefulSpiritScroll()); break;
            }

            base.OnDeath(CorpseLoot);
        }

        public override int GetAngerSound()
        {
            if (!Controlled)
                return 0x16A;

            return base.GetAngerSound();
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
