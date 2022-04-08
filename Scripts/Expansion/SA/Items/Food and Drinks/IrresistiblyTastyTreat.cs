using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public class IrresistiblyTastyTreat : TastyTreat
    {
        public override double Bonus => 0.15;
        public override TimeSpan Duration => TimeSpan.FromMinutes(20);
        public override TimeSpan CoolDown => TimeSpan.FromMinutes(120);
        public override int DamageBonus => 10;

        [Constructible]
        public IrresistiblyTastyTreat() : this(1)
        {
        }

        [Constructible]
        public IrresistiblyTastyTreat(int amount)
        {
            Stackable = true;
            Amount = amount;
        }

        public IrresistiblyTastyTreat(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1113005;  //Irresistibly Tasty Treat

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = (InheritsItem ? 0 : reader.ReadInt()); //Required for TastyTreat Insertion
        }
    }
}