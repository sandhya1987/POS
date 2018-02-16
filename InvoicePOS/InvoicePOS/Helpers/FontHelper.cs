using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InvoicePOS.Helpers
{
    class FontHelper
    {
        public static FontStretch getFontStretch(string stretch)
        {
            FontStretch fontStretch;
            switch (stretch)
            {
                case "Condensed":
                    fontStretch = FontStretches.Condensed;
                    break;
                case "SemiCondensed":
                    fontStretch = FontStretches.SemiCondensed;
                    break;
                case "ExtraCondensed":
                    fontStretch = FontStretches.ExtraCondensed;
                    break;
                case "UltraCondensed":
                    fontStretch = FontStretches.UltraCondensed;
                    break;
                case "Expanded":
                    fontStretch = FontStretches.Expanded;
                    break;
                case "SemiExpanded":
                    fontStretch = FontStretches.SemiExpanded;
                    break;
                case "ExtraExpanded":
                    fontStretch = FontStretches.ExtraExpanded;
                    break;
                case "UltraExpanded":
                    fontStretch = FontStretches.UltraExpanded;
                    break;
                case "Medium":
                    fontStretch = FontStretches.Medium;
                    break;
                case "Normal":
                    fontStretch = FontStretches.Normal;
                    break;
                default:
                    fontStretch = new FontStretch();
                    break;
            }

            return fontStretch;
        }


        public static FontStyle getFontStyle(string style)
        {
            FontStyle fontStyle;
            switch (style)
            {
                case "Italic":
                    fontStyle = FontStyles.Italic;
                    break;
                case "Normal":
                    fontStyle = FontStyles.Normal;
                    break;
                case "Oblique":
                    fontStyle = FontStyles.Oblique;
                    break;
                default:
                    fontStyle = new FontStyle();
                    break;
            }

            return fontStyle;
        }

        public static FontWeight getFontWeight(int weight)
        {
            FontWeight fontWeight;
            if (weight > 0 && weight < 1000)
            {
                fontWeight = FontWeight.FromOpenTypeWeight(weight);
            }
            else
            {
                fontWeight = new FontWeight();
            }
            return fontWeight;
        }

        public static TextDecorationCollection getTextDecorations(bool TEXT_DECOR_UNDER_LINE, bool TEXT_DECOR_BASE_LINE, bool TEXT_DECOR_STRIKE_THROUGH, bool TEXT_DECOR_OVER_LINE)
        {
            TextDecorationCollection txtDecCol = new TextDecorationCollection();

            if (TEXT_DECOR_UNDER_LINE)
            {
                txtDecCol.Add(TextDecorations.Underline);
            }
            if (TEXT_DECOR_BASE_LINE)
            {
                txtDecCol.Add(TextDecorations.Baseline);
            }
            if (TEXT_DECOR_STRIKE_THROUGH)
            {
                txtDecCol.Add(TextDecorations.Strikethrough);
            }
            if (TEXT_DECOR_OVER_LINE)
            {
                txtDecCol.Add(TextDecorations.OverLine);
            }
            return txtDecCol;
        }
    }
}
