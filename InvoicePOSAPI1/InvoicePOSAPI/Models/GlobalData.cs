using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicePOSAPI.Models
{
    public static class GlobalData
    {

        public static string gblSearchData;
        public static string gblOrderId;
        public static string gblCrpName;
        public static string gblTempOrderId;
        public static string gblPageStartIndex;
        //public static string gblApiAdress = "http://115.115.196.29:8089";
        //  public static string gblApiAdress = "http://10.10.10.6:80/Admin//";
        // public static string gblApiAdress = "http://216.226.146.81:90//";
        // public static string gblApiAdress = "http://216.226.146.81:96//";
          public static string gblApiAdress = "http://localhost:1543/";
        //public static string gblApiAdress = "http://localhost:1719//";
        public static int count;
        public static string gblSelectedAtrrBtn;
        public static string gblSearchColour;
        // Add global data shape ,style,price ,Size,Material
        public static string gblSearchShape;
        public static string gblSearchhStyle;
        public static string gblSearchPrice;
        public static string gblSearchSize;
        public static string gblSearchMeterial;
        public static string gblSearchSKU;
        public static bool gblIsSearch;
        public static int gblCount;
        public static string gblShowAllStyle;
        /// <summary>
        /// /////////////////////////////////////////////////

        public static string gblTotal;
        public static decimal TotalAmount;
        public static decimal totalOption;
        public static Boolean gblPromo;
        public static string gblShipAddr;
        public static decimal gblTax;
        public static string gblCrpID;
        public static string gblStoreName;
        public static string gblStoreID;
        public static bool gbladdWARRANTY;
        public static int glbSelectedIndex;
        public static string glbmackId;
        public static string glbPage;
        public static bool YesNo;
        public static int glbIndex;
        public static bool loaderStrat = true;
        public static long glbTotalRecord;
        public static string gblShpape;
        public static bool glbBackload;
        public static string gblHelpPage;
        public static string gblPromocode;
        public static string gblDiscountName;
        public static decimal gblTotalAmount;
        public static decimal gblDiscountAmount;
        //store the Taxamount value
        public static decimal gblTaxPercent;
        public static bool gblAllRugs;
        public static bool gblstyle = false;
        public static bool gblcolor = false;
        public static bool gblShapeSize = false;
        public static string gblemailaddress;
        public static int pageCount;
        public static int sorttype;
        public static string gblTrackFeature;
        public static string gblmsgAlert = "You have just touched the HOME/RESET button. This will clear your current shopping cart and return you to the Home Page. Are you sure you want to clear your cart and restart your shopping?";

    }
}