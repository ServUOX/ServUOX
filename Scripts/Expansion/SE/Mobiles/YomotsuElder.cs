using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a wrinkly yomotsu corpse")]
    public class YomotsuElder : BaseCreature
    {
        [Constructible]
        public YomotsuElder()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a yomotsu elder";
            Body = 255;
            BaseSoundID = 0x452;

            SetStr(686, 830);
            SetDex(251, 365);
            SetInt(17, 31);

            SetHits(801, 900);

            SetDamage(19, 27);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 65, 85);
            SetResist(ResistType.Fire, 30, 50);
            SetResist(ResistType.Cold, 45, 65);
            SetResist(ResistType.Pois, 35, 55);
            SetResist(ResistType.Engy, 25, 50);

            SetSkill(SkillName.Anatomy, 115.1, 130.0);
            SetSkill(SkillName.MagicResist, 100.1, 120.0);
            SetSkill(SkillName.Tactics, 115.1, 130.0);
            SetSkill(SkillName.Wrestling, 110.1, 130.0);

            Fame = 12000;
            Karma = -12000;

            SetWeaponAbility(WeaponAbility.DoubleStrike);
        }

        public YomotsuElder(Serial serial)
            : base(serial)
        {
        }

        public override FoodType FavoriteFood => FoodType.Fish;
        public override int Meat => 1;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 5;

        public override int GetIdleSound() { return 0x42A; }
        public override int GetAttackSound() { return 0x435; }
        public override int GetHurtSound() { return 0x436; }
        public override int GetDeathSound() { return 0x43A; }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Gems, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new GreenGourd());
            CorpseLoot.DropItem(new ExecutionersAxe());

            switch (Utility.Random(3))
            {
                case 0:
                    CorpseLoot.DropItem(new LongPants());
                    break;
                case 1:
                    CorpseLoot.DropItem(new ShortPants());
                    break;
            }

            switch (Utility.Random(6))
            {
                case 0:
                    CorpseLoot.DropItem(new Shoes());
                    break;
                case 1:
                    CorpseLoot.DropItem(new Sandals());
                    break;
                case 2:
                    CorpseLoot.DropItem(new Boots());
                    break;
                case 3:
                    CorpseLoot.DropItem(new ThighBoots());
                    break;
            }

            if (Core.SE && Utility.RandomDouble() < .25)
                CorpseLoot.DropItem(Engines.Plants.Seed.RandomBonsaiSeed());

            base.OnDeath(CorpseLoot);
        }

        // TODO: Axe Throw
        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.1 > Utility.RandomDouble())
            {
                /* Maniacal laugh
                * Cliloc: 1070840
                * Effect: Type: "3" From: "0x57D4F5B" To: "0x0" ItemId: "0x37B9" ItemIdName: "glow" FromLocation: "(884 715, 10)" ToLocation: "(884 715, 10)" Speed: "10" Duration: "5" FixedDirection: "True" Explode: "False"
                * Paralyzes for 4 seconds, or until hit
                */
                defender.FixedEffect(0x37B9, 10, 5);
                defender.SendLocalizedMessage(1070840); // You are frozen as the creature laughs maniacally.

                defender.Paralyze(TimeSpan.FromSeconds(4.0));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
