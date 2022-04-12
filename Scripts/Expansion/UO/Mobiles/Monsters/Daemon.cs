using Server.Ethics;
using Server.Factions;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a daemon corpse")]
    public class Daemon : BaseCreature
    {
        [Constructible]
        public Daemon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("daemon");
            Body = 9;
            BaseSoundID = 357;

            SetStr(476, 505);
            SetDex(76, 95);
            SetInt(301, 325);

            SetHits(286, 303);

            SetDamage(7, 14);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 45, 60);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 70.1, 80.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.MagicResist, 85.1, 95.0);
            SetSkill(SkillName.Tactics, 70.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 58;

            ControlSlots = Core.SE ? 4 : 5;
        }

        public Daemon(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 125.0;
        public override double DispelFocus => 45.0;
        public override Faction FactionAllegiance => Shadowlords.Instance;
        public override Ethic EthicAllegiance => Ethic.Evil;
        public override bool CanRummageCorpses => true;
        public override Poison PoisonImmunity => Poison.Regular;
        public override int TreasureMapLevel => 4;
        public override int Meat => 1;
        public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.ML)
            {
                switch (Utility.Random(20))
                {
                    case 0:
                        CorpseLoot.DropItem(new LichFormScroll());
                        break;
                    case 1:
                        CorpseLoot.DropItem(new PoisonStrikeScroll());
                        break;
                    case 2:
                        CorpseLoot.DropItem(new StrangleScroll());
                        break;
                    case 3:
                        CorpseLoot.DropItem(new VengefulSpiritScroll());
                        break;
                    case 4:
                        CorpseLoot.DropItem(new WitherScroll());
                        break;
                }
            }

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
