using DatabaseLayer.Book;
using DatabaseLayer.User;
using System;
using System.Collections.Generic;
using System.Text;


namespace DatabaseLayer.Cart
{
    public class CartPostModel
    {

        //public RegisterPostModel registerPostModel { get; set; }
        //public BookPostModel bookPostModel { get; set; }
        public string bookId { get; set; }
        public int quantity { get; set; }
    }
}
