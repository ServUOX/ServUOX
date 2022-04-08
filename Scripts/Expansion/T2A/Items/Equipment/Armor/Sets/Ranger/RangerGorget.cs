using Server.Engines.Craft;

namespace Server.Items
{
    public class RangerGorget : BaseArmor, IRepairable
    {
        public CraftSystem RepairSystem => DefTailoring.CraftSystem;

        public override bool IsArtifact => true;
        [Constructible]
        public RangerGorget()
            : base(0x13D6)
        {
            Weight = 1.0;
            Hue = 0x59C;
        }

        public RangerGorget(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 2;
        public override int BaseFireResistance => 4;
        public override int BaseColdResistance => 3;
        public override int BasePoisonResistance => 3;
        public override int BaseEnergyResistance => 4;
        public override int InitMinHits => 35;
        public override int InitMaxHits => 45;
        public override int AosStrReq => 25;
        public override int OldStrReq => 25;
        public override int ArmorBase => 16;
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
        public override CraftResource DefaultResource => CraftResource.RegularLeather;
        public override int LabelNumber => 1041495;// studded gorget, ranger armor

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
