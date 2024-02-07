namespace BookStoreApp.Core.Entities.RequestFeatures
{
    public class MetaData
    {
        public int CurrentPage { get; set; } // mevcut sayfa
        public int TotalPage { get; set; } // toplam sayfa
        public int PageSize { get; set; } // sayfabaşıan kayıt
        public int TotalCount { get; set; } // toplam kayıt
        public bool HasPrevious => CurrentPage > 1; // mevcut sayfa 1'den büyükse kendisinden önce sayfa var olarak yani "true" döneceğiz.Yoksa False
        // Sonrasında Sayfa Var Mı ? HasNextPage 
        public bool HasNextPage => CurrentPage < TotalPage; // mevcut sayfa toplam sayfa'dan küçükse "True"

    }
}
