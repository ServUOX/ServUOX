using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a ronin corpse")]
    public class Ronin : BaseCreature
    {
        public override bool ClickTitle => false;

        private DateTime m_NextWeaponChange;

        [Constructible]
        public Ronin() : base(AIType.AI_Samurai, FightMode.Closest, 10, 1, 0.3, 0.6)
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Name = "a ronin";
            Body = ((Female = Utility.RandomBool()) ? Body = 0x191 : Body = 0x190);

            Hue = Utility.RandomSkinHue();

            SetStr(326, 375);
            SetDex(31, 45);
            SetInt(101, 110);

            SetHits(301, 400);
            SetMana(101, 110);

            SetDamage(ResistType.Phys, 90, 0, 17, 25);
            SetDamage(ResistType.Pois, 10);

            SetResist(ResistType.Phys, 55, 75);
            SetResist(ResistType.Fire, 40, 60);
            SetResist(ResistType.Cold, 35, 55);
            SetResist(ResistType.Pois, 50, 70);
            SetResist(ResistType.Engy, 55, 75);

            SetSkill(SkillName.MagicResist, 42.6, 57.5);
            SetSkill(SkillName.Tactics, 115.1, 130.0);
            SetSkill(SkillName.Wrestling, 92.6, 107.5);
            SetSkill(SkillName.Anatomy, 110.1, 125.0);

            SetSkill(SkillName.Fencing, 92.6, 107.5);
            SetSkill(SkillName.Macing, 92.6, 107.5);
            SetSkill(SkillName.Swords, 92.6, 107.5);

            SetSkill(SkillName.Bushido, 95.0, 120.0);

            Fame = 8500;
            Karma = -8500;

            AddItem(new SamuraiTabi());
            AddItem(new LeatherHiroSode());
            AddItem(new LeatherDo());

            switch (Utility.Random(4))
            {
                case 0: AddItem(new LightPlateJingasa()); break;
                case 1: AddItem(new ChainHatsuburi()); break;
                case 2: AddItem(new DecorativePlateKabuto()); break;
                case 3: AddItem(new LeatherJingasa()); break;
                default: AddItem(new LeatherJingasa()); break;
            }

            switch (Utility.Random(3))
            {
                case 0: AddItem(new StuddedHaidate()); break;
                case 1: AddItem(new LeatherSuneate()); break;
                case 2: AddItem(new PlateSuneate()); break;
                default: AddItem(new PlateSuneate()); break;
            }

            if (Utility.RandomDouble() > .2)
                AddItem(new NoDachi());
            else
                AddItem(new Halberd());

            PackItem(new Wakizashi());
            PackItem(new Longsword());

            Utility.AssignRandomHair(this);

            SetWeaponAbility(WeaponAbility.RidingSwipe);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            c.DropItem(new BookOfBushido());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, 2);
        }

        public override bool AlwaysMurderer => true;
        public override bool BardImmunity => true;
        public override bool CanRummageCorpses => true;

        public override double WeaponAbilityChance
        {
            get
            {
                if (Combatant is Mobile mobile && mobile.Mounted)
                    return 0.8;

                return base.WeaponAbilityChance;
            }
        }

        private void ChangeWeapon()
        {
            if (Backpack == null)
                return;

            Item item = FindItemOnLayer(Layer.OneHanded);

            if (item == null)
                item = FindItemOnLayer(Layer.TwoHanded);

            System.Collections.Generic.List<BaseWeapon> weapons = new System.Collections.Generic.List<BaseWeapon>();

            foreach (Item i in Backpack.Items)
            {
                if (i is BaseWeapon weapon && i != item)
                    weapons.Add(weapon);
            }

            if (weapons.Count > 0)
            {
                if (item != null)
                    Backpack.DropItem(item);

                AddItem(weapons[Utility.Random(weapons.Count)]);

                m_NextWeaponChange = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));
            }
        }

        public override void OnThink()
        {
            base.OnThink();

            if (Combatant != null && m_NextWeaponChange < DateTime.UtcNow)
                ChangeWeapon();
        }

        public Ronin(Serial serial) : base(serial)
        {
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

            m_NextWeaponChange = DateTime.UtcNow;
        }
    }
}
