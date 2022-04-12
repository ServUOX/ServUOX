using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an goblin corpse")]
    public class EnslavedGreenGoblin : BaseCreature
    {
        [Constructible]
        public EnslavedGreenGoblin()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an enslaved green goblin";
            Body = 334;
            BaseSoundID = 0x600;

            SetStr(326, 326);
            SetDex(71, 71);
            SetInt(126, 126);

            SetHits(184, 184);
            SetStam(71, 71);
            SetMana(126, 126);

            SetDamage(5, 7);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 40, 40);
            SetResist(ResistType.Fire, 38, 39);
            SetResist(ResistType.Cold, 31, 32);
            SetResist(ResistType.Pois, 12, 12);
            SetResist(ResistType.Engy, 10, 11);

            SetSkill(SkillName.MagicResist, 121.6, 122.9);
            SetSkill(SkillName.Tactics, 80.0, 81.2);
            SetSkill(SkillName.Anatomy, 82.0, 83.4);
            SetSkill(SkillName.Wrestling, 99.2, 99.4);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;

            // Loot - 30-40gold, magicitem,gem,goblin blood, essence control
            switch (Utility.Random(20))
            {
                case 0:
                    PackItem(new Scimitar());
                    break;
                case 1:
                    PackItem(new Katana());
                    break;
                case 2:
                    PackItem(new WarMace());
                    break;
                case 3:
                    PackItem(new WarHammer());
                    break;
                case 4:
                    PackItem(new Kryss());
                    break;
                case 5:
                    PackItem(new Pitchfork());
                    break;
            }

            PackItem(new ThighBoots());

            switch (Utility.Random(3))
            {
                case 0:
                    PackItem(new Ribs());
                    break;
                case 1:
                    PackItem(new Shaft());
                    break;
                case 2:
                    PackItem(new Candle());
                    break;
            }

            if (0.2 > Utility.RandomDouble())
                PackItem(new BolaBall());
        }

        public EnslavedGreenGoblin(Serial serial)
            : base(serial)
        {
        }

        public override int GetAngerSound() { return 0x600; }
        public override int GetIdleSound() { return 0x600; }
        public override int GetAttackSound() { return 0x5FD; }
        public override int GetHurtSound() { return 0x5FF; }
        public override int GetDeathSound() { return 0x5FE; }

        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => 1;
        public override OppositionGroup OppositionGroup => OppositionGroup.SavagesAndOrcs;

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
            int version = reader.ReadInt();
        }
    }
}
