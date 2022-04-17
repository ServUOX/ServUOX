using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a slimey corpse")]
    public class CorrosiveSlime : Slime
    {
        [Constructible]
        public CorrosiveSlime()
            : base()
        {
            Name = "a corrosive slime";

            switch (Utility.Random(14))
            {
                case 1: Hue = 2205; break;
                case 2: Hue = 2206; break;
                case 3: Hue = 2207; break;
                case 4: Hue = 2209; break;
                case 5: Hue = 2210; break;
                case 6: Hue = 2211; break;
                case 7: Hue = 2213; break;
                case 8: Hue = 2214; break;
                case 9: Hue = 2215; break;
                case 10: Hue = 2216; break;
                case 11: Hue = 2218; break;
                case 12: Hue = 2219; break;
                case 13: Hue = 2222; break;
                case 14: Hue = 2223; break;
                default: Hue = 2215; break;
            }

            SetStr(22, 34);
            SetDex(16, 21);
            SetInt(16, 20);

            SetHits(15, 19);

            SetDamage(ResistType.Phys, 100, 0, 1, 5);

            SetResist(ResistType.Phys, 5, 10);
            SetResist(ResistType.Pois, 15, 20);

            SetSkill(SkillName.Poisoning, 36.0, 49.1);
            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 15.9, 18.9);
            SetSkill(SkillName.Tactics, 24.6, 26.1);
            SetSkill(SkillName.Wrestling, 24.9, 26.1);

            Fame = 300;
            Karma = -300;

            VirtualArmor = 8;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 23.1;

            SetMagicalAbility(MagicalAbility.Poisoning);
        }

        public CorrosiveSlime(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Regular;
        public override Poison HitPoison => Poison.Regular;
        public override FoodType FavoriteFood => FoodType.Fish;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLoot(LootPack.Gems);
        }

        public override void OnDeath(Container c)
        {
            if (!Controlled && Map != null && Map != Map.TerMur && Utility.Random(10) == 0)
            {
                Item item;
                switch (Utility.Random(3))
                {
                    case 0: item = new GelatanousSkull(); break;
                    case 1: item = new CoagulatedLegs(); break;
                    case 2: item = new PartiallyDigestedTorso(); break;
                    default:
                        item = new PartiallyDigestedTorso();
                        break;
                }

                if (item != null)
                    c.DropItem(item);
            }

            base.OnDeath(c);
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
