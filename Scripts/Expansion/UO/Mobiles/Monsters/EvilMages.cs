using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an evil mage corpse")]
    public class EvilMage : BaseCreature
    {
        [Constructible]
        public EvilMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("evil mage");
            Title = "the evil mage";

            var robe = new Robe(Utility.RandomNeutralHue());
            var sandals = new Sandals();

            if (!Core.UOTD)
            {
                Body = Race.Human.MaleBody;

                AddItem(robe);
                AddItem(sandals);
            }
            else
            {
                Body = 124;

                // NEED TO SET A HUE SKIN IS STARK WHITE!!

                PackItem(robe);
                PackItem(sandals);
            }

            SetStr(81, 105);
            SetDex(91, 115);
            SetInt(96, 120);

            SetHits(49, 63);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.EvalInt, 75.1, 100.0);
            SetSkill(SkillName.Magery, 75.1, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 97.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 20.2, 60.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 16;
            PackItem(Loot.PackReg(6));
        }

        public override int GetDeathSound()
        {
            return 0x423;
        }

        public override int GetHurtSound()
        {
            return 0x436;
        }

        public EvilMage(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses => true;
        public override bool AlwaysMurderer => true;
        public override int Meat => 1;
        public override int TreasureMapLevel => Core.AOS ? 1 : 0;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.MedScrolls);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            AddItem(new WizardsHat(Utility.RandomMetalHue()));

            if (Core.SA)
            {
                switch (Utility.Random(16))
                {
                    case 0: PackItem(new BloodOathScroll()); break;
                    case 1: PackItem(new CurseWeaponScroll()); break;
                    case 2: PackItem(new StrangleScroll()); break;
                }
            }
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

    [CorpseName("an evil mage lord corpse")]
    public class EvilMageLord : BaseCreature
    {
        [Constructible]
        public EvilMageLord()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            var robe = new Robe(Utility.RandomNeutralHue());
            var sandals = new Sandals();

            Name = NameList.RandomName("evil mage lord");

            if (!Core.UOTD)
            {
                Body = Race.Human.MaleBody;

                AddItem(robe);
                AddItem(sandals);
            }
            else
            {
                Body = Utility.RandomList(125, 126);

                // NEED TO SET A HUE SKIN IS STARK WHITE!!

                PackItem(robe);
                PackItem(sandals);
            }

            SetStr(81, 105);
            SetDex(191, 215);
            SetInt(126, 150);

            SetHits(49, 63);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 40);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 80.2, 100.0);
            SetSkill(SkillName.Magery, 95.1, 100.0);
            SetSkill(SkillName.Meditation, 27.5, 50.0);
            SetSkill(SkillName.MagicResist, 77.5, 100.0);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 20.3, 80.0);

            Fame = 10500;
            Karma = -10500;

            VirtualArmor = 16;

            PackItem(Loot.PackReg(23));
        }

        public override int GetDeathSound()
        {
            return 0x423;
        }

        public override int GetHurtSound()
        {
            return 0x436;
        }

        public EvilMageLord(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses => true;
        public override bool AlwaysMurderer => true;
        public override int Meat => 1;
        public override int TreasureMapLevel => Core.AOS ? 2 : 0;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            AddItem(new WizardsHat(Utility.RandomMetalHue()));

            if (Core.SA)
            {
                switch (Utility.Random(16))
                {
                    case 0: PackItem(new BloodOathScroll()); break;
                    case 1: PackItem(new CurseWeaponScroll()); break;
                    case 2: PackItem(new StrangleScroll()); break;
                    case 3: PackItem(new LichFormScroll()); break;
                }
            }
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
