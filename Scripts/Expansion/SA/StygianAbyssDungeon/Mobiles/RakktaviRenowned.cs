using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("Rakktavi [Renowned] corpse")]
    public class RakktaviRenowned : BaseRenowned
    {
        [Constructible]
        public RakktaviRenowned()
            : base(AIType.AI_Archer)
        {
            Name = "Rakktavi";
            Title = "[Renowned]";
            Body = 0x8E;
            BaseSoundID = 437;

            SetStr(119);
            SetDex(279);
            SetInt(327);

            SetHits(50000);
            SetMana(327);
            SetStam(279);

            SetDamage(8, 10);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 20, 30);
            SetResist(ResistType.Fire, 10, 25);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Poison, 10, 20);
            SetResist(ResistType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.Archery, 80.1, 90.0);
            SetSkill(SkillName.MagicResist, 66.0);
            SetSkill(SkillName.Tactics, 68.1);
            SetSkill(SkillName.Wrestling, 85.5);

            Fame = 6500;
            Karma = -6500;

            VirtualArmor = 56;

            AddItem(new Bow());
            PackItem(new Arrow(Utility.RandomMinMax(10, 30)));
        }

        public RakktaviRenowned(Serial serial)
            : base(serial)
        {
        }
        public override bool AllureImmunity => true;
        public override Type[] UniqueSAList => new Type[] { };
        public override Type[] SharedSAList => new Type[] { typeof(CavalrysFolly), typeof(TorcOfTheGuardians) };
        public override InhumanSpeech SpeechType => InhumanSpeech.Ratman;
        public override bool CanRummageCorpses => true;
        public override int Hides => 8;
        public override HideType HideType => HideType.Spined;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 3);
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