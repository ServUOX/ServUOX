using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a wolf spider corpse")]
    public class WolfSpider : BaseCreature
    {
        [Constructible]
        public WolfSpider()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a wolf spider";
            Body = 736;
            Hue = 0;

            SetStr(225, 268);
            SetDex(145, 165);
            SetInt(285, 310);

            SetHits(150, 160);
            SetMana(285, 310);
            SetStam(145, 165);

            SetDamage(15, 18);

            SetDamageType(ResistType.Phys, 70);
            SetDamageType(ResistType.Pois, 30);

            SetResist(ResistType.Phys, 30, 35);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.Anatomy, 80.0, 90.0);
            SetSkill(SkillName.MagicResist, 60.0, 75.0);
            SetSkill(SkillName.Poisoning, 62.3, 77.2);
            SetSkill(SkillName.Tactics, 84.1, 95.9);
            SetSkill(SkillName.Wrestling, 80.2, 90.0);
            SetSkill(SkillName.Hiding, 105.0, 110.0);
            SetSkill(SkillName.Stealth, 105.0, 110.0);

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 59.1;

            // VirtualArmor?

            PackItem(new SpidersSilk(8));
        }

        public WolfSpider(Serial serial)
            : base(serial)
        {
        }

        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Arachnid;
        public override Poison PoisonImmunity => Poison.Regular;
        public override Poison HitPoison => Poison.Regular;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (!Controlled && Utility.RandomDouble() < 0.01)
                CorpseLoot.DropItem(new LuckyCoin());

            base.OnDeath(CorpseLoot);
        }

        public override int GetIdleSound() { return 1605; }
        public override int GetAngerSound() { return 1602; }
        public override int GetHurtSound() { return 1604; }
        public override int GetDeathSound() { return 1603; }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version == 0)
            {
                Hue = 0;
                Body = 736;
            }

            if (version == 1 && (AbilityProfile == null || AbilityProfile.MagicalAbility == MagicalAbility.None))
            {
                SetMagicalAbility(MagicalAbility.Poisoning);
            }
        }
    }
}
