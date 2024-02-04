namespace BookStoreApp.Core.DTOs.Concretes.BookDTOs
{
    public record BookDto
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public decimal Price { get; init; }
    }

    //Data Transfer Objelerin Temel Özellikleri : 
    // Değeri  değişmemelidir, eğer değişecekse yenisi oluşturulmalıdır.
    // referans tiplidir.
    // Linq'a açıktır.
    // Yukarıda da görüleceği üzere hem record yaptık hemde tanımlandığı yerde değerinin verilmesi için set yerine init; olarak belirttik.
    // init  = readonly 



}
