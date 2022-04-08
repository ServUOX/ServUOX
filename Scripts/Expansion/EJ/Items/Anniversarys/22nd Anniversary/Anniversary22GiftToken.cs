using System;
using Server;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
    public class Anniversary22GiftToken : Item, IRewardOption
    {
        public override int LabelNumber => 1159145;  // 22nd Anniversary Gift Token

        [Constructible]
        public Anniversary22GiftToken()
            : base(0x4BC6)
        {
            Hue = 1286;
            LootType = LootType.Blessed;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(RewardOptionGump));
                from.SendGump(new RewardOptionGump(this, 1156888));
            }
            else
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
            }
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(1, 1159146); // Copper Wings
            list.Add(2, 1159147); // Copper Portraits
            list.Add(3, 1159148); // Copper Ship Relief
            list.Add(4, 1159149); // Copper Sunflower
        }


        public void OnOptionSelected(Mobile from, int choice)
        {
            var bag = new Bag
            {
                Hue = 1286
            };

            bool chance = Utility.RandomDouble() < .1;

            Item item;

            switch (choice)
            {
                default:
                    bag.Delete();
                    break;
                case 1:
                    {
                        item = new CopperWings();

                        if (chance)
                        {
                            item.Hue = 2951;
                        }

                        bag.DropItem(item);
                        from.AddToBackpack(bag);
                        Delete();
                        break;
                    }
                case 2:
                    {
                        item = new CopperPortrait1();

                        if (chance)
                        {
                            item.Hue = 2951;
                        }

                        bag.DropItem(item);

                        item = new CopperPortrait2();

                        if (chance)
                        {
                            item.Hue = 2951;
                        }

                        bag.DropItem(item);

                        from.AddToBackpack(bag);
                        Delete();
                        break;
                    }

                case 3:
                    {
                        item = new CopperShipReliefAddonDeed();

                        if (chance)
                        {
                            item.Hue = 2951;
                        }

                        bag.DropItem(item);

                        from.AddToBackpack(bag);
                        Delete();
                        break;
                    }
                case 4:
                    item = new CopperSunflower();

                    if (chance)
                    {
                        item.Hue = 2951;
                    }

                    bag.DropItem(item);

                    from.AddToBackpack(bag);
                    Delete();
                    break;
            }
        }

        public Anniversary22GiftToken(Serial serial)
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
            _ = reader.ReadInt();
        }
    }

    public class AnniversaryGiver22nd : GiftGiver
    {
        public static void Initialize()
        {
            GiftGiving.Register(new AnniversaryGiver22nd());
        }

        public override DateTime Start => new DateTime(2019, 09, 01);
        public override DateTime Finish => new DateTime(2019, 10, 18);
        public override TimeSpan MinimumAge => TimeSpan.FromDays(30);

        public override void GiveGift(Mobile mob)
        {
            Item token = new Anniversary22GiftToken();

            switch (GiveGift(mob, token))
            {
                case GiftResult.Backpack:
                    mob.SendLocalizedMessage(1159142); // Happy 22nd Anniversary! We have placed a gift for you in your backpack.
                    break;
                case GiftResult.BankBox:
                    mob.SendLocalizedMessage(1159143); // Happy 22nd Anniversary! We have placed a gift for you in your bank box. 
                    break;
            }
        }
    }
}
