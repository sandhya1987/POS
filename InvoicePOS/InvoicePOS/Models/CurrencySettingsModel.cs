using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Resources;
using System.Collections;
using System.Collections.Generic;
using System.IO;



namespace InvoicePOS.Models
{
    [Serializable()]
    public class CurrencySettingsModel
    {

        public string                   FONT_FAMILY { get; set; }
        public int                      FONT_WEIGHT { get; set; }
        public string                   FONT_STYLE { get; set; }
        public string                   FONT_STRETCH { get; set; }
        public double                   FONT_SIZE { get; set; }
        public bool                     TEXT_DECOR_UNDER_LINE { get; set; }
        public bool                     TEXT_DECOR_BASE_LINE { get; set; }
        public bool                     TEXT_DECOR_STRIKE_THROUGH { get; set; }
        public bool                     TEXT_DECOR_OVER_LINE { get; set; }
        public string                   NORMAL_CURRENCY_SYMBOL { get; set; }
        public bool                     NORMAL_CURRENCY_SYMBOL_LEFT { get; set; }
        public string                   SPECIAL_CURRENCY_SYMBOL { get; set; }
        public bool                     SPECIAL_CURRENCY_SYMBOL_LEFT { get; set; }
        public bool                     NUMBER_FORMAT_CHOICE_2 { get; set; }
        public int                      NUMBER_DECIMAL_PLACES { get; set; }
        public int                      UNIT_PRICE_DECIMAL_PLACES { get; set; }
        public int                      QUANTITY_DECIMAL_PLACES { get; set; }
        public int                      CODE_PAGE { get; set; }

        private string _NUMBER_FORMAT;
        public string NUMBER_FORMAT
        {
            get
            {
                string format = "";
                if (NUMBER_FORMAT_CHOICE_2)
                {
                    format = "###,##0";
                }
                else
                {
                    format = "##,##0";
                }

                if (NUMBER_DECIMAL_PLACES > 0)
                {
                    format += ".";
                    for (int i = 0; i < NUMBER_DECIMAL_PLACES; i++)
                    {
                        format += "0";
                    }
                }
                return format;
            }
        }

        private string _QUANTITY_FORMAT;
        public string QUANTITY_FORMAT
        {
            get
            {
                string format = "";
                if (NUMBER_FORMAT_CHOICE_2)
                {
                    format = "###,##0";
                }
                else
                {
                    format = "##,##0";
                }

                if (QUANTITY_DECIMAL_PLACES > 0)
                {
                    format += ".";
                    for (int i = 0; i < QUANTITY_DECIMAL_PLACES; i++)
                    {
                        format += "0";
                    }
                }
                return format;
            }
        }

        private string _UNIT_PRICE_FORMAT;
        public string UNIT_PRICE_FORMAT
        {
            get
            {
                string format = "";
                if (NUMBER_FORMAT_CHOICE_2)
                {
                    format = "###,##0";
                }
                else
                {
                    format = "##,##0";
                }

                if (UNIT_PRICE_DECIMAL_PLACES > 0)
                {
                    format += ".";
                    for (int i = 0; i < UNIT_PRICE_DECIMAL_PLACES; i++)
                    {
                        format += "0";
                    }
                }
                return format;
            }
        }

        public void LoadSettings()
        {

            string resxFile = "..\\..\\Resources\\Settings.resx";

            if (File.Exists(resxFile))
            {
                using (ResXResourceReader resxReader = new ResXResourceReader(resxFile))
                {
                    foreach (DictionaryEntry entry in resxReader)
                    {
                        string s = (string)entry.Key;

                        switch (s)
                        {
                            case "FONT_FAMILY":
                                FONT_FAMILY = (string)entry.Value;
                                break;
                            case "FONT_WEIGHT":
                                FONT_WEIGHT = (int)entry.Value;
                                break;
                            case "FONT_STYLE":
                                FONT_STYLE = (string)entry.Value;
                                break;
                            case "FONT_STRETCH":
                                FONT_STRETCH = (string)entry.Value;
                                break;
                            case "FONT_SIZE":
                                FONT_SIZE = (double)entry.Value;
                                break;
                            case "TEXT_DECOR_UNDER_LINE":
                                TEXT_DECOR_UNDER_LINE = (bool)entry.Value;
                                break;
                            case "TEXT_DECOR_STRIKE_THROUGH":
                                TEXT_DECOR_STRIKE_THROUGH = (bool)entry.Value;
                                break;
                            case "TEXT_DECOR_OVER_LINE":
                                TEXT_DECOR_OVER_LINE = (bool)entry.Value;
                                break;
                            case "TEXT_DECOR_BASE_LINE":
                                TEXT_DECOR_BASE_LINE = (bool)entry.Value;
                                break;
                            case "NORMAL_CURRENCY_SYMBOL":
                                NORMAL_CURRENCY_SYMBOL = (string)entry.Value;
                                break;
                            case "NORMAL_CURRENCY_SYMBOL_LEFT":
                                NORMAL_CURRENCY_SYMBOL_LEFT = (bool)entry.Value;
                                break;
                            case "SPECIAL_CURRENCY_SYMBOL":
                                SPECIAL_CURRENCY_SYMBOL = (string)entry.Value;
                                break;
                            case "SPECIAL_CURRENCY_SYMBOL_LEFT":
                                SPECIAL_CURRENCY_SYMBOL_LEFT = (bool)entry.Value;
                                break;
                            case "NUMBER_FORMAT_CHOICE_2":
                                NUMBER_FORMAT_CHOICE_2 = (bool)entry.Value;
                                break;
                            case "NUMBER_DECIMAL_PLACES":
                                NUMBER_DECIMAL_PLACES = (int)entry.Value;
                                break;
                            case "UNIT_PRICE_DECIMAL_PLACES":
                                UNIT_PRICE_DECIMAL_PLACES = (int)entry.Value;
                                break;
                            case "QUANTITY_DECIMAL_PLACES":
                                QUANTITY_DECIMAL_PLACES = (int)entry.Value;
                                break;
                            case "CODE_PAGE":
                                CODE_PAGE = (int)entry.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }


    }
}
