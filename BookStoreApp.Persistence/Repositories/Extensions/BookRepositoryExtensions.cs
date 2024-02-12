using BookStoreApp.Core.Entities;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
namespace BookStoreApp.Persistence.Repositories.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>

            books.Where(book => (book.Price >= minPrice) &&
            (book.Price <= maxPrice));
        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            // eğer anahtar kelime (searchTerm) boş ise
            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;

            // 1- öncelikle parametre olarak gelen anahtar kelimeyi küçük karakterlere çevirip daha sonra başında ve sonunda varsa boşluklarını kaldıralım.

            var lowerCaseTerm = searchTerm.Trim().ToLower(); // kelime

            // 2- daha sonra db'den gelecek olarak varlıkların title'ını küçük harfe dönüştürüp içerisinde searchTerm ile eşleşenleri döneceğiz.

            return books.Where(book => book.Title.ToLower().Contains(lowerCaseTerm));



        }
        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            // 1- Öncelikle parametre olarak gelen orderByQueryString ifadesinin boş olup olmadığına bakacağız.
            //      a. eğer boş ise default olarak Id'ye göre sıralama yapacağız.

            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(book => book.Id);

            // 2- Daha sonra orderByQueryString ifadesinin başından ve sonundan boşlukları kaldırıp "," ' e göre bölmek istediğimizi ve parametrelerden bir array elde etmek istediğimizi söyleyeceğiz. ["title desc","price","id desc"]
            //      a. bunu yapmamızdaki asıl ama. Client yapacağı request'te önce title'a göre sonra price'a göre sıralama talebinde bulunabilir ve isteği aşağıdaki gibi olabilir.
                        // -->  books?orderBy=title,price

            var orderParams = orderByQueryString.Trim().Split(',');

            // 3- Sonraki adımda Reflection kullanrak ilgili nesnenin yani Book classının propertylerini dinamik olarak elde edeceğiz. Elde ederken erişim bildirgeçi Public olan ve newlenebilenleri seçeceğiz
                    // a. nedir bunlar Id, Price, Title
            var propertyInfos = typeof(Book).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 4- Şimdi ise parametre olarka aldığımız  sorgu ifadelerini içeren array olan orderParams ile propertyInfos'u eşleştireceğiz.
            //      // a.virgülüne göre ayrılan ifadelere ilişkin array yapısı üzerinde gezeceğiz ve her birine param diyeceğiz, param boşsa onu atlayıp diğer indexe geçeceğiz
            // b. ilk param yani parametre için (title olduğunu varsayalım )  döngüyü başlatalım.

            // döngü sonucunda üreteceğimiz son değer string bir değer olacak ve bunu "price desc" şeklinde yeni bir string üreterek buna ekleyeceğiz.
            var orderQueryBuilder = new StringBuilder();

            foreach ( var param in orderParams)   // döngü sonunda son olarak elimizde artık title ascending, price descending, id ascending gibi bir ifade olacak
            {
                if(string.IsNullOrWhiteSpace(param))
                    continue;

                //b. param dolu ise yani ["","price desc"] 0'ıncı indexi atladık boş diye. ama 1'inci index dolu "price desc" olarak gelmiş ve boşluğu olacağı için bununda boşluğuna göre ayırıp almamız gerekmektedir. çünkü property'den sonraki yani price'dan sonraki "desc" ifadesi sıralamanın yönünü belirlediği için buna göre yönlendireceğiz desc ise z-a sıralama asc a-z sıralama yapacağız. 
                //// b.1 boşluğa göre ayırdık. ["price","desc"] arrayi elde ettik ve 0'ıncı indexteki propertyName'i "price"'ı aldık.
                var propertyFromQueryName = param.Split(' ')[0];

                //c. Daha sonra yukarıda reflection yardımıyla aldığımız Object Propertylerinde böyle bir property varmı yokmu ona göre kontrolümüzü yapalım.

                var objectProperty = propertyInfos.FirstOrDefault(propertyInfo => propertyInfo.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                    /// DipNot :  StringComparison.InvariantCultureIgnoreCase  büyük ve küçük harf uyumuna bakılmasını istemediğimizi belirttik.

                // 5. Bu durumda artık queryString olarak aldığımız property name'ler ile db'de yer alan Book nesnesine ait PropertyName eşleştirmesini gerçekleştirdik.
                //      a. Eğer eşleşirse parametre olarak verilen propertyName demekki db'de ki varlığın propertylerinde mevcut tabi bunun kontrolünü yapalım
                if (objectProperty == null)
                    continue;
                //      b. property eşleştiğine göre şimdi de property'nin yönünü belirleyelim. Yönünü belirleyebilmek için öncelikle param ifadesine odaklanalım sonuçta orada "price desc" şeklindeki ifadeler yalın halde duruyor, burada bu ifadenin son ifadesine yani "desc" veya "asc" neyse 1'inci indexleri alalım.

                //      c. Eğer desc ise azalan asc ise artan sıralama olacağı için yönü buna göre tayin edeceğiz.
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                // Yönü de tayin ettiğimize göre yeni bir StringBuilder tanımlayıp ayırdığımız her iki ifadeyi birleştirelim.
                // ["price","desc"] bundan "price descending" olarak bir string üretmemiz gerekmektedir.

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}");

                // son olarak elimizde artık title ascending, price descending, id ascending gibi bir ifade olacak


            }

            // son oluşan ifadedeki virgülleri kaldırıp bunun yerine boşluk ekleyelim.

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (orderQuery is null)
                return books.OrderBy(b => b.Id);

            return books.OrderBy(orderQuery); // burada yeni bir kütüphaneye ihtiyacımız var aksi takdirde OrderBy() parametre olarak edxpression beklemektedir, vermez isek hata fırlatacaktır.
                                              // bu paketin adı System.Linq.Dynamic.Core son sürümüdür.

        }
    }
}
