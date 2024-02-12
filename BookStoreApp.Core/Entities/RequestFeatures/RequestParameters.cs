namespace BookStoreApp.Core.Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;

        // Auto-implemented property : Yazma var okuma var ancak herhangi bir logic yok.
        public int PageNumber { get; set; }

        // Full Property  :  Okuma var ancak Yazma Set işleminde logic işletiliyorsa Full Property olarak tanımlanmaktadır.
        private int _pageSize;  

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; } // kullanıcı 50 ve üzeri kayıt görmek ister ise  buna izin vermeyip üst limitimiz olan 50 kayıt listeleyeceğiz, ancak 50 limitinden küçük bir değerde kayıt görmek isterse o zaman istediği adette kayıt listeleyeceğiz.
        }

        public String? OrderBy { get; set; }

    }
}
