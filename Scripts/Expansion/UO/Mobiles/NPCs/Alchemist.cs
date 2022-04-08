using Server.Engines.BulkOrders;
using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Alchemist : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructible]
        public Alchemist()
            : base("the alchemist")
        {
            SetSkill(SkillName.Alchemy, 85.0, 100.0);
            SetSkill(SkillName.TasteID, 65.0, 88.0);
        }
        public override BODType BODType => BODType.Alchemy;

        public override bool IsValidBulkOrder(Item item)
        {
            return (item is SmallAlchemyBOD || item is LargeAlchemyBOD);
        }

        public override bool SupportsBulkOrders(Mobile from)
        {
            return BulkOrderSystem.NewSystemEnabled && from is PlayerMobile && from.Skills[SkillName.Alchemy].Base > 0;
        }

        public override void OnSuccessfulBulkOrderReceive(Mobile from)
        {
            if (from is PlayerMobile)
            {
                ((PlayerMobile)from).NextAlchemyBulkOrder = TimeSpan.Zero;
            }
        }

        public Alchemist(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild => NpcGuild.MagesGuild;
        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBAlchemist(this));
        }

        public override void InitOutfit()
        {
            base.InitOutfit();
            AddItem(new Items.Robe(Utility.RandomPinkHue()));
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

    public class SBAlchemist : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo;
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBAlchemist(Mobile m)
        {
            if (m != null)
            {
                m_BuyInfo = new InternalBuyInfo(m);
            }
        }

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo(Mobile m)
            {
                Add(new GenericBuyInfo<RefreshPotion>(15, 10, 0xF0B, 0, true));
                Add(new GenericBuyInfo<AgilityPotion>(15, 10, 0xF08, 0, true));
                Add(new GenericBuyInfo<NightSightPotion>(15, 10, 0xF06, 0, true));
                Add(new GenericBuyInfo<LesserHealPotion>(15, 10, 0xF0C, 0, true));
                Add(new GenericBuyInfo<StrengthPotion>(15, 10, 0xF09, 0, true));
                Add(new GenericBuyInfo<LesserPoisonPotion>(15, 10, 0xF0A, 0, true));
                Add(new GenericBuyInfo<LesserCurePotion>(15, 10, 0xF07, 0, true));
                Add(new GenericBuyInfo<LesserExplosionPotion>(21, 10, 0xF0D, 0, true));
                Add(new GenericBuyInfo<MortarPestle>(8, 10, 0xE9B, 0));

                Add(new GenericBuyInfo<BlackPearl>(5, 20, 0xF7A, 0));
                Add(new GenericBuyInfo<Bloodmoss>(5, 20, 0xF7B, 0));
                Add(new GenericBuyInfo<Garlic>(3, 20, 0xF84, 0));
                Add(new GenericBuyInfo<Ginseng>(3, 20, 0xF85, 0));
                Add(new GenericBuyInfo<MandrakeRoot>(3, 20, 0xF86, 0));
                Add(new GenericBuyInfo<Nightshade>(3, 20, 0xF88, 0));
                Add(new GenericBuyInfo<SpidersSilk>(3, 20, 0xF8D, 0));
                Add(new GenericBuyInfo<SulfurousAsh>(3, 20, 0xF8C, 0));

                Add(new GenericBuyInfo<EmptyBottle>(5, 100, 0xF0E, 0, true));
                Add(new GenericBuyInfo<HeatingStand>(2, 100, 0x1849, 0));
                Add(new GenericBuyInfo<SkinTingeingTincture>(1255, 20, 0xEFF, 90));

                if (m.Map != Map.TerMur)
                {
                    Add(new GenericBuyInfo<HairDye>(37, 10, 0xEFF, 0));
                }
                else if (m is Zosilem)
                {
                    Add(new GenericBuyInfo<GlassblowingBook>(10637, 30, 0xFF4, 0));
                    Add(new GenericBuyInfo<SandMiningBook>(10637, 30, 0xFF4, 0));
                    Add(new GenericBuyInfo<Blowpipe>(21, 100, 0xE8A, 0x3B9));
                }
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(BlackPearl), 3);
                Add(typeof(Bloodmoss), 3);
                Add(typeof(MandrakeRoot), 2);
                Add(typeof(Garlic), 2);
                Add(typeof(Ginseng), 2);
                Add(typeof(Nightshade), 2);
                Add(typeof(SpidersSilk), 2);
                Add(typeof(SulfurousAsh), 2);
                Add(typeof(EmptyBottle), 3);
                Add(typeof(MortarPestle), 4);
                Add(typeof(HairDye), 19);

                Add(typeof(NightSightPotion), 7);
                Add(typeof(AgilityPotion), 7);
                Add(typeof(StrengthPotion), 7);
                Add(typeof(RefreshPotion), 7);
                Add(typeof(LesserCurePotion), 7);
                Add(typeof(CurePotion), 11);
                Add(typeof(GreaterCurePotion), 15);
                Add(typeof(LesserHealPotion), 7);
                Add(typeof(HealPotion), 11);
                Add(typeof(GreaterHealPotion), 15);
                Add(typeof(LesserPoisonPotion), 7);
                Add(typeof(PoisonPotion), 9);
                Add(typeof(GreaterPoisonPotion), 13);
                Add(typeof(DeadlyPoisonPotion), 21);
                Add(typeof(LesserExplosionPotion), 10);
                Add(typeof(ExplosionPotion), 15);
                Add(typeof(GreaterExplosionPotion), 25);
            }
        }
    }
}
