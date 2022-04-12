namespace Server.Mobiles
{
    [CorpseName("a clan ribbon supplicant corpse")]
    public class ClanRS : BaseCreature
    {
        //public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Ratman; } }
        [Constructible]
        public ClanRS()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Clan Ribbon Supplicant";
            Body = 42;
            Hue = 2952;
            BaseSoundID = 437;

            SetStr(173);
            SetDex(117);
            SetInt(207);

            SetHits(127);

            SetDamage(7, 14);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 55, 60);
            SetResist(ResistType.Fire, 30, 35);
            SetResist(ResistType.Cold, 80, 85);
            SetResist(ResistType.Pois, 45, 50);
            SetResist(ResistType.Engy, 25, 30);

            SetSkill(SkillName.MagicResist, 78.5, 80.0);
            SetSkill(SkillName.Tactics, 62.1, 65.0);
            SetSkill(SkillName.Wrestling, 56.5, 60.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 48;
        }

        public ClanRS(Serial serial)
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
