using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("Vitavi [Renowned] corpse")]
    public class VitaviRenowned : BaseRenowned
    {
        [Constructible]
        public VitaviRenowned()
            : base(AIType.AI_Mystic)
        {
            Name = "Vitavi";
            Title = "[Renowned]";
            Body = 0x8F;
            BaseSoundID = 437;

            SetStr(300, 350);
            SetDex(250, 300);
            SetInt(300, 350);

            SetHits(45000, 50000);

            SetDamage(7, 14);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 50, 60);
            SetResist(ResistType.Fire, 30, 50);
            SetResist(ResistType.Cold, 60, 80);
            SetResist(ResistType.Poison, 20, 30);
            SetResist(ResistType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 70.1, 80.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.MagicResist, 75.1, 100.0);
            SetSkill(SkillName.Tactics, 70.1, 75.0);
            SetSkill(SkillName.Wrestling, 50.1, 75.0);

            Fame = 7500;
            Karma = -7500;

            VirtualArmor = 44;

            PackItem(Loot.PackReg(6));
        }

        public VitaviRenowned(Serial serial)
            : base(serial)
        {
        }

        public override Type[] UniqueSAList => new Type[] { };

        public override Type[] SharedSAList => new[] { typeof(AxeOfAbandon), typeof(DemonBridleRing), typeof(VoidInfusedKilt) };

        public override InhumanSpeech SpeechType => InhumanSpeech.Ratman;
        public override bool AllureImmunity => true;

        public override bool CanRummageCorpses => true;

        public override int Meat => 1;

        public override int Hides => 8;

        public override HideType HideType => HideType.Spined;

        public override void OnDeath(Container c)
        {
            if (0.02 > Utility.RandomDouble())
                c.DropItem(Loot.RandomStatue());
            base.OnDeath(c);
        }


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
            var version = reader.ReadInt();
        }
    }
}
