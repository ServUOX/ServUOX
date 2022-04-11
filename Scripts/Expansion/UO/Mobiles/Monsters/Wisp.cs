using System;
using Server.Factions;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("a wisp corpse")]
    public class Wisp : BaseCreature
    {
        [Constructible]
        public Wisp()
            : base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a wisp";
            Body = 58;
            BaseSoundID = 466;

            SetStr(196, 225);
            SetDex(196, 225);
            SetInt(196, 225);

            SetHits(118, 135);

            SetDamage(17, 18);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Energy, 50);

            SetResist(ResistanceType.Physical, 35, 45);
            SetResist(ResistanceType.Fire, 20, 40);
            SetResist(ResistanceType.Cold, 10, 30);
            SetResist(ResistanceType.Poison, 5, 10);
            SetResist(ResistanceType.Energy, 50, 70);

            SetSkill(SkillName.EvalInt, 80.0);
            SetSkill(SkillName.Magery, 80.0);
            SetSkill(SkillName.MagicResist, 80.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Wrestling, 80.0);

            Fame = 4000;
            Karma = 0;

            VirtualArmor = 40;

            AddItem(new LightSource());
        }

        public Wisp(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType => InhumanSpeech.Wisp;
        public override Faction FactionAllegiance => CouncilOfMages.Instance;
        public override Ethics.Ethic EthicAllegiance => Ethics.Ethic.Hero;
        public override TimeSpan ReacquireDelay => TimeSpan.FromSeconds(1.0);

        public override TribeType Tribe => TribeType.Fey;

        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.ML && Utility.RandomDouble() < .33)
                CorpseLoot.DropItem(Engines.Plants.Seed.RandomPeculiarSeed(4));

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
