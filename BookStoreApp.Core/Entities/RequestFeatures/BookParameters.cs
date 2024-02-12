namespace BookStoreApp.Core.Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
    {
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; } = 1000;
        public bool ValidPriceRange => MaxPrice > MinPrice; // max fiyat min fiyattan büyükse true, eğilse hatalı parametreden dolayı false olarak dönüş yapacağız.
        public String? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "id";
        }
    }
}
