using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an air elemental corpse")]
    public class SummonedAirElemental : BaseCreature
    {
        [Constructible]
        public SummonedAirElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an air elemental";
            Body = 13;
            Hue = 0x4001;
            BaseSoundID = 655;

            SetStr(200);
            SetDex(200);
            SetInt(100);

            SetHits(150);
            SetStam(50);

            SetDamage(ResistType.Phys, 50, 0, 6, 9);
            SetDamage(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 70, 80);

            SetSkill(SkillName.Meditation, 90.0);
            SetSkill(SkillName.EvalInt, 70.0);
            SetSkill(SkillName.Magery, 70.0);
            SetSkill(SkillName.MagicResist, 60.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 80.0);

            VirtualArmor = 40;
            ControlSlots = 2;
        }

        public SummonedAirElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;

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

    [CorpseName("a daemon corpse")]
    public class SummonedDaemon : BaseCreature
    {
        [Constructible]
        public SummonedDaemon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("daemon");
            Body = Core.AOS ? 10 : 9;
            BaseSoundID = 357;

            SetStr(200);
            SetDex(110);
            SetInt(150);

            SetDamage(ResistType.Phys, 0, 0, 14, 21);
            SetDamage(ResistType.Pois, 100);

            SetResist(ResistType.Phys, 45, 55);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 70, 80);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 98.1, 99.0);

            VirtualArmor = 58;
            ControlSlots = Core.SE ? 4 : 5;
        }

        public SummonedDaemon(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 125.0;
        public override double DispelFocus => 45.0;
        public override Poison PoisonImmunity => Poison.Regular;// TODO: Immune to poison?
        public override bool CanFly => true;

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

    [CorpseName("an earth elemental corpse")]
    public class SummonedEarthElemental : BaseCreature
    {
        [Constructible]
        public SummonedEarthElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an earth elemental";
            Body = 14;
            BaseSoundID = 268;

            SetStr(200);
            SetDex(70);
            SetInt(70);

            SetHits(180);

            SetDamage(ResistType.Phys, 100, 0, 14, 21);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.MagicResist, 65.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 90.0);

            VirtualArmor = 34;
            ControlSlots = 2;
        }

        public SummonedEarthElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;

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

    [CorpseName("a fire elemental corpse")]
    public class SummonedFireElemental : BaseCreature
    {
        [Constructible]
        public SummonedFireElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a fire elemental";
            Body = 15;
            BaseSoundID = 838;

            SetStr(200);
            SetDex(200);
            SetInt(100);

            SetDamage(ResistType.Phys, 0, 0, 9, 14);
            SetDamage(ResistType.Fire, 100);

            SetResist(ResistType.Phys, 50, 60);
            SetResist(ResistType.Fire, 70, 80);
            SetResist(ResistType.Cold, 0, 10);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.EvalInt, 90.0);
            SetSkill(SkillName.Magery, 90.0);
            SetSkill(SkillName.MagicResist, 85.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 92.0);

            VirtualArmor = 40;
            ControlSlots = 4;

            AddItem(new LightSource());
        }

        public SummonedFireElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;

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

    [CorpseName("a water elemental corpse")]
    public class SummonedWaterElemental : BaseCreature
    {
        [Constructible]
        public SummonedWaterElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a water elemental";
            Body = 16;
            BaseSoundID = 278;

            SetStr(200);
            SetDex(70);
            SetInt(100);

            SetHits(165);

            SetDamage(ResistType.Phys, 0, 0, 12, 16);
            SetDamage(ResistType.Cold, 100);

            SetResist(ResistType.Phys, 50, 60);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 70, 80);
            SetResist(ResistType.Pois, 45, 55);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Meditation, 90.0);
            SetSkill(SkillName.EvalInt, 80.0);
            SetSkill(SkillName.Magery, 80.0);
            SetSkill(SkillName.MagicResist, 75.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 85.0);

            VirtualArmor = 40;
            ControlSlots = 3;
            CanSwim = true;
        }

        public SummonedWaterElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;

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
