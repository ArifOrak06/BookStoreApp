﻿namespace BookStoreApp.Core.ResponseResultPattern.Exceptions
{
    public sealed class BookNotMatchedException : NotMatchedException
    {
        public BookNotMatchedException(int id) : base($"Parametre olarak gönderilen varlık id :  {id} ile Request Body içerisinde gönderilen nesne id'leri eşleşmemektedir.")
        {
        }
    }
}
