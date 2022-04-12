using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("Tikitavi [Renowned] corpse")]
    public class TikitaviRenowned : BaseRenowned
    {
        [Constructible]
        public TikitaviRenowned()
            : base(AIType.AI_Melee)
        {
            Name = "Tikitavi";
            Title = "[Renowned]";
            Body = 42;
            BaseSoundID = 437;

            SetStr(315, 354);
            SetDex(139, 177);
            SetInt(243, 288);

            SetHits(50000);
            SetMana(243, 288);
            SetStam(139, 177);

            SetDamage(7, 9);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 26, 28);
            SetResist(ResistType.Fire, 22, 25);
            SetResist(ResistType.Cold, 30, 38);
            SetResist(ResistType.Pois, 14, 17);
            SetResist(ResistType.Engy, 15, 18);

            SetSkill(SkillName.MagicResist, 40.4);
            SetSkill(SkillName.Tactics, 73.6);
            SetSkill(SkillName.Wrestling, 66.5);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;
        }

        public TikitaviRenowned(Serial serial)
            : base(serial)
        {
        }
        public override bool AllureImmunity => true;

        public override Type[] UniqueSAList => new Type[] { typeof(BasiliskHideBreastplate) };
        public override Type[] SharedSAList => new Type[] { typeof(LegacyOfDespair), typeof(MysticsGarb) };
        public override InhumanSpeech SpeechType => InhumanSpeech.Ratman;
        public override bool CanRummageCorpses => true;
        public override int Hides => 8;
        public override HideType HideType => HideType.Spined;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
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