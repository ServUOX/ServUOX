namespace Server.Gumps
{
    public class FortuneGump : Gump
    {
        public FortuneGump(string text)
            : base(200, 200)
        {
            AddPage(0);

            AddImage(0, 0, 0x7724);

            int one, two, three;

            one = Utility.RandomMinMax(1, 19);
            two = Utility.RandomMinMax(one + 1, 20);
            three = Utility.RandomMinMax(0, one - 1);

            AddImageTiled(28, 140, 115, 180, 0x7725 + one);
            AddTooltip(GetTooltip(one));
            AddHtmlLocalized(28, 115, 125, 20, 1076079, 0x7FFF, false, false); // The Past
            AddImageTiled(171, 140, 115, 180, 0x7725 + two);
            AddTooltip(GetTooltip(two));
            AddHtmlLocalized(171, 115, 125, 20, 1076081, 0x7FFF, false, false); // The Question
            AddImageTiled(314, 140, 115, 180, 0x7725 + three);
            AddTooltip(GetTooltip(three));
            AddHtmlLocalized(314, 115, 125, 20, 1076080, 0x7FFF, false, false); // The Future

            AddHtml(30, 32, 400, 25, text, true, false);
        }

        private int GetTooltip(int number)
        {
            if (number > 9)
                return 1076015 + number - 10;

            switch (number)
            {
                case 0:
                    return 1076063;
                case 1:
                    return 1076060;
                case 2:
                    return 1076061;
                case 3:
                    return 1076057;
                case 4:
                    return 1076062;
                case 5:
                    return 1076059;
                case 6:
                    return 1076058;
                case 7:
                    return 1076065;
                case 8:
                    return 1076064;
                case 9:
                    return 1076066;
                default:
                    break;
            }

            return 1052009; // I have seen the error of my ways!
        }
    }
}
