using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Pixie [Renowned] corpse")]
    public class PixieRenowned : BaseRenowned
    {
        [Constructible]
        public PixieRenowned()
            : base(AIType.AI_Mage)
        {
            Name = "Pixie";
            Title = "[Renowned]";
            Body = 128;
            BaseSoundID = 0x467;

            SetStr(-350, 380);
            SetDex(450, 600);
            SetInt(700, 850);

            SetHits(9100, 9200);
            SetStam(450, 600);
            SetMana(700, 800);

            SetDamage(27, 38);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 70, 90);
            SetResist(ResistanceType.Fire, 60, 70);
            SetResist(ResistanceType.Cold, 70, 80);
            SetResist(ResistanceType.Poison, 60, 70);
            SetResist(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.EvalInt, 100.0, 100.0);
            SetSkill(SkillName.Magery, 90.1, 110.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.MagicResist, 110.5, 150.0);
            SetSkill(SkillName.Tactics, 100.1, 120.0);
            SetSkill(SkillName.Wrestling, 100.1, 120.0);

            Fame = 7000;
            Karma = 7000;

            VirtualArmor = 100;
        }

        public PixieRenowned(Serial serial)
            : base(serial)
        {
        }

        public override Type[] UniqueSAList => new Type[] { typeof(DemonHuntersStandard), typeof(DragonJadeEarrings) };
        public override Type[] SharedSAList => new Type[] { typeof(PillarOfStrength), typeof(SwordOfShatteredHopes) };
        public override bool InitialInnocent => true;
        public override HideType HideType => HideType.Spined;
        public override int Hides => 5;
        public override int Meat => 1;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (0.02 > Utility.RandomDouble())
                CorpseLoot.DropItem(Loot.RandomStatue());

            if (Utility.RandomDouble() < 0.3)
                CorpseLoot.DropItem(new PixieLeg());

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
