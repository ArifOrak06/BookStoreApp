namespace BookStoreApp.Core.ResponseResultPattern.Exceptions
{
    public class PriceOutOfRangeBadRequestException : BadRequestException // Fiyat Aralığı belirlediğimiz maximimum değer sınırından büyük bir değer client tarafından parametre                                                                            olarak gönderildiği durumlarda  fırlatılacak Exception
    {
        public PriceOutOfRangeBadRequestException() : base("Maximum price should be less than 1000 and greater than 10.") 
        {

        }
    }
}
