using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("a ratman's corpse")]
    public class Ratman : BaseCreature
    {
        [Constructible]
        public Ratman()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("ratman");
            Body = 42;
            BaseSoundID = 437;

            SetStr(96, 120);
            SetDex(81, 100);
            SetInt(36, 60);

            SetHits(58, 72);

            SetDamage(4, 5);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 25, 30);
            SetResist(ResistanceType.Fire, 10, 20);
            SetResist(ResistanceType.Cold, 10, 20);
            SetResist(ResistanceType.Poison, 10, 20);
            SetResist(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.MagicResist, 35.1, 60.0);
            SetSkill(SkillName.Tactics, 50.1, 75.0);
            SetSkill(SkillName.Wrestling, 50.1, 75.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;
        }

        public Ratman(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Ratman;
        public override bool CanRummageCorpses => true;
        public override int Hides => 8;
        public override HideType HideType => HideType.Spined;

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

    [CorpseName("a ratman archer corpse")]
    public class RatmanArcher : BaseCreature
    {
        [Constructible]
        public RatmanArcher()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("ratman");
            Body = 0x8E;
            BaseSoundID = 437;

            SetStr(146, 180);
            SetDex(101, 130);
            SetInt(116, 140);

            SetHits(88, 108);

            SetDamage(4, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 40, 55);
            SetResist(ResistanceType.Fire, 10, 20);
            SetResist(ResistanceType.Cold, 10, 20);
            SetResist(ResistanceType.Poison, 10, 20);
            SetResist(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 60.2, 100.0);
            SetSkill(SkillName.Archery, 80.1, 90.0);
            SetSkill(SkillName.MagicResist, 65.1, 90.0);
            SetSkill(SkillName.Tactics, 50.1, 75.0);
            SetSkill(SkillName.Wrestling, 50.1, 75.0);

            Fame = 6500;
            Karma = -6500;

            VirtualArmor = 56;

            AddItem(new Bow());
            PackItem(new Arrow(Utility.RandomMinMax(50, 70)));
        }

        public RatmanArcher(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Ratman;
        public override bool CanRummageCorpses => true;
        public override int Hides => 8;
        public override HideType HideType => HideType.Spined;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
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

    [CorpseName("a glowing ratman corpse")]
    public class RatmanMage : BaseCreature
    {
        [Constructible]
        public RatmanMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("ratman");
            Body = 0x8F;
            BaseSoundID = 437;

            SetStr(146, 180);
            SetDex(101, 130);
            SetInt(186, 210);

            SetHits(88, 108);

            SetDamage(7, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 40, 45);
            SetResist(ResistanceType.Fire, 10, 20);
            SetResist(ResistanceType.Cold, 10, 20);
            SetResist(ResistanceType.Poison, 10, 20);
            SetResist(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.EvalInt, 70.1, 80.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.MagicResist, 65.1, 90.0);
            SetSkill(SkillName.Tactics, 50.1, 75.0);
            SetSkill(SkillName.Wrestling, 50.1, 75.0);

            Fame = 7500;
            Karma = -7500;

            VirtualArmor = 44;

            PackItem(Loot.PackReg(6));
        }

        public RatmanMage(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 2;
        public override InhumanSpeech SpeechType => InhumanSpeech.Ratman;
        public override bool CanRummageCorpses => true;
        public override int Hides => 8;
        public override HideType HideType => HideType.Spined;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.LowScrolls);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.AOS)
            {
                switch (Utility.Random(60))
                {
                    case 0: CorpseLoot.DropItem(new AnimateDeadScroll()); break;
                    case 1: CorpseLoot.DropItem(new BloodOathScroll()); break;
                    case 2: CorpseLoot.DropItem(new CorpseSkinScroll()); break;
                    case 3: CorpseLoot.DropItem(new CurseWeaponScroll()); break;
                    case 4: CorpseLoot.DropItem(new EvilOmenScroll()); break;
                    case 5: CorpseLoot.DropItem(new HorrificBeastScroll()); break;
                    case 6: CorpseLoot.DropItem(new MindRotScroll()); break;
                    case 7: CorpseLoot.DropItem(new PainSpikeScroll()); break;
                    case 8: CorpseLoot.DropItem(new WraithFormScroll()); break;
                    case 9: CorpseLoot.DropItem(new PoisonStrikeScroll()); break;
                }
            }

            if (Utility.RandomDouble() < 0.02)
                CorpseLoot.DropItem(Loot.RandomStatue());

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
