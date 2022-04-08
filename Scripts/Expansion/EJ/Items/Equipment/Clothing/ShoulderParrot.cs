using System;

namespace Server.Items
{
    [Flipable(0xA2CA, 0xA2CB)]
    public class ShoulderParrot : BaseOuterTorso
    {
        private DateTime INextFly;
        private DateTime IFlyEnd;
        private Timer ITimer;
        private Mobile ILastShoulder;

        private string IMasterName;

        [CommandProperty(AccessLevel.GameMaster)]
        public string MasterName { get => IMasterName; set { IMasterName = value; InvalidateProperties(); } }

        [Constructible]
        public ShoulderParrot()
            : base(0xA2CA)
        {
            LootType = LootType.Blessed;
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (IMasterName != null)
            {
                list.Add(1158958, $"{IMasterName}{(IMasterName.ToLower().EndsWith("s") || IMasterName.ToLower().EndsWith("z") ? "'" : "'s")}");
            }
            else
            {
                list.Add(1158928); // Shoulder Parrot
            }
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m.FindItemOnLayer(Layer.OuterTorso) == this)
            {
                if (INextFly > DateTime.UtcNow)
                {
                    m.SendLocalizedMessage(1158956); // Your parrot is too tired to fly right now.
                }
                else
                {
                    ITimer = Timer.DelayCall(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500), FlyOnTick);
                    ITimer.Start();

                    Movable = false;
                    ILastShoulder = m;
                    MoveToWorld(new Point3D(m.X, m.Y, m.Z + 15), m.Map);
                    ItemID = 0xA2CC;

                    IFlyEnd = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(3, 5));
                }
            }
            else
            {
                m.SendLocalizedMessage(1158957); // Your parrot can't fly here.
            }
        }

        private void FlyOnTick()
        {
            if (IFlyEnd < DateTime.UtcNow)
            {
                Movable = true;
                ItemID = 0xA2CA;

                if (ILastShoulder.FindItemOnLayer(Layer.OuterTorso) != null)
                {
                    ILastShoulder.Backpack.DropItem(this);
                }
                else
                {
                    ILastShoulder.AddItem(this);
                }

                ILastShoulder = null;
                ITimer.Stop();
                INextFly = DateTime.UtcNow + TimeSpan.FromMinutes(2);
            }
        }

        public ShoulderParrot(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(IMasterName);
            writer.Write(ILastShoulder);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();

            IMasterName = reader.ReadString();
            Mobile m = reader.ReadMobile();

            if (m != null)
            {
                ItemID = 0xA2CA;

                Timer.DelayCall(() =>
                {
                    if (m.FindItemOnLayer(Layer.OuterTorso) != null)
                    {
                        m.Backpack.DropItem(this);
                    }
                    else
                    {
                        m.AddItem(this);
                    }
                });
            }
        }
    }
}
