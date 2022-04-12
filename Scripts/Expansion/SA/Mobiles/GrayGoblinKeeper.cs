using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a goblin keeper corpse")]
    public class GrayGoblinKeeper : BaseCreature
    {
        [Constructible]
        public GrayGoblinKeeper()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a gray goblin keeper";

            Body = 723;
            Hue = 1900;
            BaseSoundID = 0x600;

            SetStr(326);
            SetDex(79);
            SetInt(114);

            SetHits(186);
            SetStam(79);
            SetMana(114);

            SetDamage(5, 7);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 45);
            SetResist(ResistType.Fire, 33);
            SetResist(ResistType.Cold, 25);
            SetResist(ResistType.Pois, 20);
            SetResist(ResistType.Engy, 10);

            SetSkill(SkillName.MagicResist, 129.9);
            SetSkill(SkillName.Tactics, 86.7);
            SetSkill(SkillName.Anatomy, 86.6);
            SetSkill(SkillName.Wrestling, 103.6);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;

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

        public GrayGoblinKeeper(Serial serial)
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
        public override TribeType Tribe => TribeType.GrayGoblin;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version == 0)
            {
                Body = 723;
                Hue = 1900;
            }
        }
    }
}
