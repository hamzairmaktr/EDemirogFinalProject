using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {

        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintinesTime = "Sistem bakımda";
        public static string ProductListed = "Ürünler listelendi";
        public static string ProductDeleted = "Ürün silindi";
        public static string ProductUpdated = "Ürün gücellendi";
        public static string UrunNameMevcutError = "Bu ürün ismine sahip başka bir ürün var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}
