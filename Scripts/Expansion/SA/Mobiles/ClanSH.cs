namespace Server.Mobiles
{
    [CorpseName("a clan scratch henchrat corpse")]
    public class ClanSH : BaseCreature
    {
        [Constructible]
        public ClanSH()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Clan Scratch Henchrat";
            Body = 42;
            BaseSoundID = 437;

            SetStr(227);
            SetDex(183);
            SetInt(93);

            SetHits(2065);

            SetDamage(5, 7);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 26, 30);
            SetResist(ResistType.Fire, 29, 35);
            SetResist(ResistType.Cold, 30, 35);
            SetResist(ResistType.Poison, 15, 20);
            SetResist(ResistType.Energy, 13, 15);

            SetSkill(SkillName.MagicResist, 35.4, 40.0);
            SetSkill(SkillName.Tactics, 61.1, 65.0);
            SetSkill(SkillName.Wrestling, 64.0, 65.0);
            SetSkill(SkillName.Anatomy, 74.0, 75.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 48;
        }

        public ClanSH(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses => true;
        public override int Hides => 8;
        public override HideType HideType => HideType.Spined;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 3);
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
