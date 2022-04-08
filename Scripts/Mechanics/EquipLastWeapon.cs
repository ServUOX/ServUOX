using Server.Mobiles;
using Server.Items;

namespace Server.Network
{
    public class EquipLastWeaponPacket
    {
        public static void Initialize()
        {
            PacketHandlers.RegisterEncoded(0x1E, true, new OnEncodedPacketReceive(EquipLastWeaponRequest));
        }

        public static void EquipLastWeaponRequest(NetState state, IEntity e, EncodedReader reader)
        {
            if (!(state.Mobile is PlayerMobile from) || from.Backpack == null)
                return;

            if (from.IsStaff() || Core.TickCount - from.NextActionTime >= 0)
            {
                BaseWeapon toEquip = from.LastWeapon;

                if (!(from.FindItemOnLayer(Layer.OneHanded) is BaseWeapon toDisarm))
                    toDisarm = from.FindItemOnLayer(Layer.TwoHanded) as BaseWeapon;

                if (toDisarm != null)
                {
                    from.Backpack.DropItem(toDisarm);
                    from.NextActionTime = Core.TickCount + Mobile.ActionDelay;
                }

                if (toEquip != toDisarm && toEquip != null && toEquip.Movable && toEquip.IsChildOf(from.Backpack))
                {
                    from.EquipItem(toEquip);
                    from.NextActionTime = Core.TickCount + Mobile.ActionDelay;
                }
            }
            else
            {
                from.SendActionMessage();
            }
        }
    }
}
