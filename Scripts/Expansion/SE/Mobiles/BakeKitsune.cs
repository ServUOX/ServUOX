using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a bake kitsune corpse")]
    public class BakeKitsune : BaseCreature
    {
        [Constructible]
        public BakeKitsune()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bake kitsune";
            Body = 246;

            SetStr(171, 220);
            SetDex(126, 145);
            SetInt(376, 425);

            SetHits(301, 350);

            SetDamage(ResistType.Phys, 70, 0, 15, 22);
            SetDamage(ResistType.Engy, 30);

            SetResist(ResistType.Phys, 40, 60);
            SetResist(ResistType.Fire, 70, 90);
            SetResist(ResistType.Cold, 40, 60);
            SetResist(ResistType.Pois, 40, 60);
            SetResist(ResistType.Engy, 40, 60);

            SetSkill(SkillName.EvalInt, 80.1, 90.0);
            SetSkill(SkillName.Magery, 80.1, 90.0);
            SetSkill(SkillName.MagicResist, 80.1, 100.0);
            SetSkill(SkillName.Tactics, 70.1, 90.0);
            SetSkill(SkillName.Wrestling, 50.1, 55.0);

            Fame = 8000;
            Karma = -8000;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 80.7;

            if (Utility.RandomDouble() < .25)
                PackItem(Engines.Plants.Seed.RandomBonsaiSeed());

            SetSpecialAbility(SpecialAbility.Rage);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override int Meat => 5;
        public override int Hides => 30;
        public override HideType HideType => HideType.Regular;
        public override FoodType FavoriteFood => FoodType.Fish;
        public override bool DisplayFameTitle => false;
        public override bool ClickTitle => false;
        public override bool PropertyTitle => false;

        public override void OnCombatantChange()
        {
            if (Combatant == null && !IsBodyMod && !Controlled && m_DisguiseTimer == null && Utility.RandomBool())
                m_DisguiseTimer = Timer.DelayCall(TimeSpan.FromSeconds(Utility.RandomMinMax(15, 30)), new TimerCallback(Disguise));
        }

        public override bool OnBeforeDeath()
        {
            RemoveDisguise();

            return base.OnBeforeDeath();
        }

        private Timer m_DisguiseTimer;

        public void Disguise()
        {
            if (Combatant != null || IsBodyMod || Controlled)
                return;

            FixedEffect(0x376A, 8, 32);
            PlaySound(0x1FE);

            Female = Utility.RandomBool();

            if (Female)
            {
                BodyMod = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                BodyMod = 0x190;
                Name = NameList.RandomName("male");
            }

            Title = "the mystic llama herder";
            Hue = Race.Human.RandomSkinHue();
            HairItemID = Race.Human.RandomHair(this);
            HairHue = Race.Human.RandomHairHue();
            FacialHairItemID = Race.Human.RandomFacialHair(this);
            FacialHairHue = HairHue;

            switch (Utility.Random(4))
            {
                case 0:
                    AddItem(new Shoes(Utility.RandomNeutralHue()));
                    break;
                case 1:
                    AddItem(new Boots(Utility.RandomNeutralHue()));
                    break;
                case 2:
                    AddItem(new Sandals(Utility.RandomNeutralHue()));
                    break;
                case 3:
                    AddItem(new ThighBoots(Utility.RandomNeutralHue()));
                    break;
                default:
                    AddItem(new Shoes(Utility.RandomNeutralHue()));
                    break;
            }

            AddItem(new Robe(Utility.RandomNondyedHue()));

            m_DisguiseTimer = null;
            m_DisguiseTimer = Timer.DelayCall(TimeSpan.FromSeconds(75), new TimerCallback(RemoveDisguise));
        }

        public void RemoveDisguise()
        {
            if (!IsBodyMod)
                return;

            Name = "a bake kitsune";
            Title = null;
            BodyMod = 0;
            Hue = 0;
            HairItemID = 0;
            HairHue = 0;
            FacialHairItemID = 0;
            FacialHairHue = 0;

            DeleteItemOnLayer(Layer.OuterTorso);
            DeleteItemOnLayer(Layer.Shoes);

            m_DisguiseTimer = null;
        }

        public void DeleteItemOnLayer(Layer layer)
        {
            Item item = FindItemOnLayer(layer);

            if (item != null)
                item.Delete();
        }

        public override int GetAngerSound() => 0x4DE;

        public override int GetIdleSound() => 0x4DD;

        public override int GetAttackSound() => 0x4DC;

        public override int GetHurtSound() => 0x4DF;

        public override int GetDeathSound() => 0x4DB;

        public BakeKitsune(Serial serial)
            : base(serial)
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
            int version = reader.ReadInt();

            Timer.DelayCall(TimeSpan.Zero, new TimerCallback(RemoveDisguise));
        }
    }
}
