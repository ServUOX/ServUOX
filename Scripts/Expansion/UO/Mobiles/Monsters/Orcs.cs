using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("an orcish corpse")]
    public class Orc : BaseCreature
    {
        [Constructible]
        public Orc()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("orc");
            Body = 17;
            BaseSoundID = 0x45A;

            SetStr(96, 120);
            SetDex(81, 105);
            SetInt(36, 60);

            SetHits(58, 72);

            SetDamage(5, 7);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 25, 30);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Poison, 10, 20);
            SetResist(ResistType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.Tactics, 55.1, 80.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;
        }

        public Orc(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Orc;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => 1;
        public override TribeType Tribe => TribeType.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new ThighBoots());

            if (Core.UOR)
            {
                if (Core.HS)
                {
                    if (Utility.RandomDouble() < 0.8)
                        CorpseLoot.DropItem(new Yeast());
                    else if (Utility.RandomDouble() < 0.8)
                        CorpseLoot.DropItem(new BolaBall());
                }
                else
                {
                    if (Utility.RandomDouble() < 0.8)
                        CorpseLoot.DropItem(new BolaBall());
                }
            }

            switch (Utility.Random(3))
            {
                case 0:
                    CorpseLoot.DropItem(new Ribs());
                    break;
                case 1:
                    CorpseLoot.DropItem(new Shaft());
                    break;
                case 2:
                    CorpseLoot.DropItem(new Candle());
                    break;
            }

            switch (Utility.Random(6))
            {
                case 0:
                    CorpseLoot.DropItem(new Scimitar());
                    break;
                case 1:
                    CorpseLoot.DropItem(new Katana());
                    break;
                case 2:
                    CorpseLoot.DropItem(new WarMace());
                    break;
                case 3:
                    CorpseLoot.DropItem(new WarHammer());
                    break;
                case 4:
                    CorpseLoot.DropItem(new Kryss());
                    break;
                case 5:
                    CorpseLoot.DropItem(new Pitchfork());
                    break;
            }

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
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
            _ = reader.ReadInt();
        }
    }

    [CorpseName("an orcish corpse")]
    public class OrcCaptain : BaseCreature
    {
        [Constructible]
        public OrcCaptain()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("orc");
            Body = 7;
            BaseSoundID = 0x45A;

            SetStr(111, 145);
            SetDex(101, 135);
            SetInt(86, 110);

            SetHits(67, 87);

            SetDamage(5, 15);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 30, 35);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Poison, 5, 10);
            SetResist(ResistType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 70.1, 85.0);
            SetSkill(SkillName.Swords, 70.1, 95.0);
            SetSkill(SkillName.Tactics, 85.1, 100.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 34;
        }

        public OrcCaptain(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Orc;
        public override bool CanRummageCorpses => true;
        public override int Meat => 1;
        public override TribeType Tribe => TribeType.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.ML && Utility.RandomDouble() < 0.05)
                CorpseLoot.DropItem(new StoutWhip());

            if (Core.AOS)
                CorpseLoot.DropItem(Loot.RandomNecromancyReagent());

            if (Core.HS && Utility.RandomDouble() < 0.5)
                CorpseLoot.DropItem(new Yeast());

            // TODO: Skull?
            switch (Utility.Random(7))
            {
                case 0:
                    CorpseLoot.DropItem(new Arrow());
                    break;
                case 1:
                    CorpseLoot.DropItem(new Lockpick());
                    break;
                case 2:
                    CorpseLoot.DropItem(new Shaft());
                    break;
                case 3:
                    CorpseLoot.DropItem(new Ribs());
                    break;
                case 4:
                    CorpseLoot.DropItem(new Bandage());
                    break;
                case 5:
                    CorpseLoot.DropItem(new BeverageBottle(BeverageType.Wine));
                    break;
                case 6:
                    CorpseLoot.DropItem(new Jug(BeverageType.Cider));
                    break;
            }

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
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

    [CorpseName("an orcish corpse")]
    public class OrcishLord : BaseCreature
    {
        [Constructible]
        public OrcishLord()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an orcish lord";
            Body = 138;
            BaseSoundID = 0x45A;

            SetStr(147, 215);
            SetDex(91, 115);
            SetInt(61, 85);

            SetHits(95, 123);

            SetDamage(4, 14);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 25, 35);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Poison, 30, 40);
            SetResist(ResistType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 70.1, 85.0);
            SetSkill(SkillName.Swords, 60.1, 85.0);
            SetSkill(SkillName.Tactics, 75.1, 90.0);
            SetSkill(SkillName.Wrestling, 60.1, 85.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 42;
        }

        public OrcishLord(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Orc;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => 1;
        public override TribeType Tribe => TribeType.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Average);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            // TODO: evil orc helm
            switch (Utility.Random(5))
            {
                case 0:
                    CorpseLoot.DropItem(new Lockpick());
                    break;
                case 1:
                    CorpseLoot.DropItem(new MortarPestle());
                    break;
                case 2:
                    CorpseLoot.DropItem(new EmptyBottle());
                    break;
                case 3:
                    CorpseLoot.DropItem(new RawRibs());
                    break;
                case 4:
                    CorpseLoot.DropItem(new Shovel());
                    break;
            }

            CorpseLoot.DropItem(new RingmailChest());

            if (Utility.RandomDouble() > 0.3)
                CorpseLoot.DropItem(Loot.RandomPossibleReagent());

            if (Core.UOR && Utility.RandomDouble() > .2)
                CorpseLoot.DropItem(new BolaBall());

            if (Core.HS && Utility.RandomDouble() > 0.5)
                CorpseLoot.DropItem(new Yeast());

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
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

    [CorpseName("a glowing orc corpse")]
    public class OrcishMage : BaseCreature
    {
        [Constructible]
        public OrcishMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an orcish mage";
            Body = 140;
            BaseSoundID = 0x45A;

            SetStr(116, 150);
            SetDex(91, 115);
            SetInt(161, 185);

            SetHits(70, 90);

            SetDamage(4, 14);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 25, 35);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Poison, 30, 40);
            SetResist(ResistType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 60.1, 72.5);
            SetSkill(SkillName.Magery, 60.1, 72.5);
            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 50.1, 65.0);
            SetSkill(SkillName.Wrestling, 40.1, 50.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 30;

            PackItem(Loot.PackReg(6));
        }

        public OrcishMage(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Orc;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => 1;
        public override TribeType Tribe => TribeType.Orc;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.ML)
            {
                switch (Utility.Random(8))
                {
                    case 0: CorpseLoot.DropItem(new CorpseSkinScroll()); break;
                }
            }

            if (Core.UOTD && Utility.RandomDouble() > 0.05)
                CorpseLoot.DropItem(new OrcishKinMask());

            if (Core.HS && Utility.RandomDouble() > 0.5)
                CorpseLoot.DropItem(new Yeast());

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
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
