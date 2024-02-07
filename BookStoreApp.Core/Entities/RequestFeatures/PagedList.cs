namespace BookStoreApp.Core.Entities.RequestFeatures
{
    public class PagedList<T> : List<T> // pagedList yapımıza List özelliği kazandırdık.
    {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> items, int entityCount, int pageNumber, int pageSize)
        {
            MetaData = new MetaData()
            {
                TotalCount = entityCount,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling(entityCount / (double)pageSize) // burada toplam sayfa sayısını bulabilmek için client'ın istediği toplam ürün sayısı / her sayfada listelenecek ürün sayısına bölersek toplam sayfa sonucunu bize dönecektir. Örneğin 100 kayıt istiyoruz her sayfada 5 adet varlık listelenecek, 100/5 = 20 toplam sayfa sayısı 20 olarak elde edeceğiz gibi...  Sonuç her zaman tam sayı çıkmayabilir bu nedenle yuvarlama işlemi yapmamız gerekiyor.

            };
            // metaData'yı setledikten sonra parametre olarak gelen itemleri PagedList'e ekleyelim.
            AddRange(items);
        }

        // PagedList'i newlememizi sağlayacak static bir üye tanımlayalım ki newlendiğinde constructordaki MetaData setlenip parametre olarak alınan itemler PagedList'e eklenebilsin.
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber-1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }


    }
}
