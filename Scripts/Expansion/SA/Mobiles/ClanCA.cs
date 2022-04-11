using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a clan chitter assistant corpse")]
    public class ClanCA : BaseCreature
    {
        //public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Ratman; } }
        [Constructible]
        public ClanCA()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Clan Chitter Assistant";
            Body = 0x8E;
            BaseSoundID = 437;

            SetStr(146, 175);
            SetDex(101, 130);
            SetInt(120, 135);

            SetHits(120, 145);

            SetDamage(4, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 23, 35);
            SetResist(ResistanceType.Fire, 20, 30);
            SetResist(ResistanceType.Cold, 30, 50);
            SetResist(ResistanceType.Poison, 15, 20);
            SetResist(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.Archery, 80.1, 90.0);
            SetSkill(SkillName.MagicResist, 81.1, 90.0);
            SetSkill(SkillName.Tactics, 53.8, 75.0);
            SetSkill(SkillName.Wrestling, 62.3, 75.0);

            Fame = 6500;
            Karma = -6500;

            VirtualArmor = 56;

            AddItem(new Bow());
            PackItem(new Arrow(Utility.RandomMinMax(50, 70)));
        }

        public ClanCA(Serial serial)
            : base(serial)
        {
        }

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

            if (Body == 42)
            {
                Body = 0x8E;
                Hue = 0;
            }
        }
    }
}
