using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an efreet corpse")]
    public class Efreet : BaseCreature
    {
        [Constructible]
        public Efreet()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an efreet";
            Body = 131;
            BaseSoundID = 768;

            SetStr(326, 355);
            SetDex(266, 285);
            SetInt(171, 195);

            SetHits(196, 213);

            SetDamage(11, 13);

            SetDamageType(ResistanceType.Physical, 0);
            SetDamageType(ResistanceType.Fire, 50);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 60, 70);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 60.1, 75.0);
            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 10000;
            Karma = -10000;

            VirtualArmor = 56;
        }

        public Efreet(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => Core.AOS ? 4 : 5;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (0.02 > Utility.RandomDouble())
            {
                switch (Utility.Random(5))
                {
                    case 0:
                        CorpseLoot.DropItem(new DaemonArms());
                        break;
                    case 1:
                        CorpseLoot.DropItem(new DaemonChest());
                        break;
                    case 2:
                        CorpseLoot.DropItem(new DaemonGloves());
                        break;
                    case 3:
                        CorpseLoot.DropItem(new DaemonLegs());
                        break;
                    case 4:
                        CorpseLoot.DropItem(new DaemonHelm());
                        break;
                }
            }
            base.OnDeath(CorpseLoot);
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

    [CorpseName("an ice elemental corpse")]
    public class IceElemental : BaseCreature, IAuraCreature
    {
        [Constructible]
        public IceElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ice elemental";
            Body = 161;
            BaseSoundID = 268;

            SetStr(156, 185);
            SetDex(96, 115);
            SetInt(171, 192);

            SetHits(94, 111);

            SetDamage(10, 21);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Cold, 75);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.EvalInt, 10.5, 60.0);
            SetSkill(SkillName.Magery, 10.5, 60.0);
            SetSkill(SkillName.MagicResist, 30.1, 80.0);
            SetSkill(SkillName.Tactics, 70.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 40;

            PackItem(new BlackPearl());
            PackItem(Loot.PackReg(3));

            SetAreaEffect(AreaEffect.AuraDamage);
        }

        public IceElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;

        public void AuraEffect(Mobile m)
        {
            m.FixedParticles(0x374A, 10, 30, 5052, Hue, 0, EffectLayer.Waist);
            m.PlaySound(0x5C6);

            m.SendLocalizedMessage(1008111, false, Name); //  : The intense cold is damaging you!
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.Gems, 2);
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

    [CorpseName("a snow elemental corpse")]
    public class SnowElemental : BaseCreature, IAuraCreature
    {
        [Constructible]
        public SnowElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a snow elemental";
            Body = 163;
            BaseSoundID = 263;

            SetStr(326, 355);
            SetDex(166, 185);
            SetInt(71, 95);

            SetHits(196, 213);

            SetDamage(11, 17);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Cold, 80);

            SetResistance(ResistanceType.Physical, 45, 55);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Cold, 60, 70);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.MagicResist, 50.1, 65.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 100.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 50;

            SetAreaEffect(AreaEffect.AuraDamage);
        }

        public SnowElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 2;

        public void AuraEffect(Mobile m)
        {
            m.FixedParticles(0x374A, 10, 30, 5052, Hue, 0, EffectLayer.Waist);
            m.PlaySound(0x5C6);

            m.SendLocalizedMessage(1008111, false, Name); //  : The intense cold is damaging you!
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new BlackPearl(3));
            CorpseLoot.DropItem(new IronOre(3));

            base.OnDeath(CorpseLoot);
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
