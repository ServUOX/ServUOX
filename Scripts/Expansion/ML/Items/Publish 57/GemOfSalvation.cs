using Server.Gumps;
using Server.Mobiles;
using System;

namespace Server.Items
{
    [TypeAlias("drNO.ThieveItems.GemOfSalvation")]
    public class GemOfSalvation : Item
    {
        public override int LabelNumber => 1094939;  // Gem of Salvation

        [Constructible]
        public GemOfSalvation()
            : base(0x1F13)
        {
            Hue = 286;
            LootType = LootType.Blessed;
        }

        public static void Initialize()
        {
            EventSink.PlayerDeath += new PlayerDeathEventHandler(PlayerDeath);
        }

        public static void PlayerDeath(PlayerDeathEventArgs args)
        {
            PlayerMobile pm = (PlayerMobile)args.Mobile;

            if (pm != null && pm.Backpack != null)
            {
                GemOfSalvation gem = pm.Backpack.FindItemByType<GemOfSalvation>();

                if (gem != null)
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
                    {
                        if (DateTime.UtcNow < pm.NextGemOfSalvationUse)
                        {
                            TimeSpan left = pm.NextGemOfSalvationUse - DateTime.UtcNow;

                            if (left >= TimeSpan.FromMinutes(1.0))
                                pm.SendLocalizedMessage(1095131, ((left.Hours * 60) + left.Minutes).ToString()); // Your spirit lacks cohesion. You must wait ~1_minutes~ minutes before invoking the power of a Gem of Salvation.
                            else
                                pm.SendLocalizedMessage(1095130, left.Seconds.ToString()); // Your spirit lacks cohesion. You must wait ~1_seconds~ seconds before invoking the power of a Gem of Salvation.
                        }
                        else
                        {
                            pm.CloseGump(typeof(ResurrectGump));
                            pm.SendGump(new GemResurrectGump(pm, gem));
                        }
                    });
                }
            }
        }

        public GemOfSalvation(Serial serial)
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
}

