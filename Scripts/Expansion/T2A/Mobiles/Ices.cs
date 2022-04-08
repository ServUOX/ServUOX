using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ice fiend corpse")]
    public class IceFiend : BaseCreature, IAuraCreature
    {
        [Constructible]
        public IceFiend()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ice fiend";
            Body = 43;
            BaseSoundID = 357;

            SetStr(376, 405);
            SetDex(176, 195);
            SetInt(201, 225);

            SetHits(226, 243);

            SetDamage(8, 19);

            SetSkill(SkillName.EvalInt, 80.1, 90.0);
            SetSkill(SkillName.Magery, 80.1, 90.0);
            SetSkill(SkillName.MagicResist, 75.1, 85.0);
            SetSkill(SkillName.Tactics, 80.1, 90.0);
            SetSkill(SkillName.Wrestling, 80.1, 100.0);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 60, 70);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 60;

            SetAreaEffect(AreaEffect.AuraDamage);
        }

        public IceFiend(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 4;
        public override int Meat => 1;
        public override bool CanFly => true;

        public void AuraEffect(Mobile m)
        {
            m.FixedParticles(0x374A, 10, 30, 5052, Hue, 0, EffectLayer.Waist);
            m.PlaySound(0x5C6);

            m.SendLocalizedMessage(1008111, false, Name); //  : The intense cold is damaging you!
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.MedScrolls, 2);
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

    [CorpseName("an ice serpent corpse")]
    [TypeAlias("Server.Mobiles.Iceserpant")]
    public class IceSerpent : BaseCreature
    {
        [Constructible]
        public IceSerpent()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a giant ice serpent";
            Body = 89;
            BaseSoundID = 219;

            SetStr(216, 245);
            SetDex(26, 50);
            SetInt(66, 85);

            SetHits(130, 147);
            SetMana(0);

            SetDamage(7, 17);

            SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Cold, 90);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 15, 25);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 27.5, 50.0);
            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 75.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 32;

        }

        public IceSerpent(Serial serial)
            : base(serial)
        {
        }

        public override bool DeathAdderCharmable => true;
        public override int Meat => 4;
        public override int Hides => 15;
        public override HideType HideType => HideType.Spined;

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(Loot.PackBodyPartOrBones());

            CorpseLoot.DropItem(Loot.RandomArmorOrShieldOrWeapon());

            if (0.025 > Utility.RandomDouble())
                CorpseLoot.DropItem(new GlacialStaff());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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

    [CorpseName("an ice snake corpse")]
    [TypeAlias("Server.Mobiles.Icesnake")]
    public class IceSnake : BaseCreature
    {
        [Constructible]
        public IceSnake()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ice snake";
            Body = 52;
            Hue = 0x480;
            BaseSoundID = 0xDB;

            SetStr(42, 54);
            SetDex(36, 45);
            SetInt(26, 30);

            SetMana(0);

            SetDamage(4, 12);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Cold, 25);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 39.3, 54.0);
            SetSkill(SkillName.Wrestling, 39.3, 54.0);

            Fame = 900;
            Karma = -900;

            VirtualArmor = 30;
        }

        public IceSnake(Serial serial)
            : base(serial)
        {
        }

        public override bool DeathAdderCharmable => true;
        public override int Meat => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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
