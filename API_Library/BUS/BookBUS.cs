using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;
using API_Library.DAO;
using API_Library.Entities;

namespace API_Library.BUS
{
    public class BookBUS: BookDAO
    {
        public List<BookEntity> GetBookEntities()
        {
            CategoryDAO categoryDAO = new CategoryDAO();
            ImageDAO imageDAO= new ImageDAO();
            PositionDAO positionDAO= new PositionDAO();
            LanguageDAO languageDAO = new LanguageDAO();
            List<BookEntity> list= new List<BookEntity>();
            List<Book> books = Get();
            for(int i =0;i < books.Count; i++)
            {
                Category category = categoryDAO.GetById((short)books[i].CategoryId);
                List<Image> images = imageDAO.GetByBookId(books[i].BookId);
                Position position = positionDAO.GetById((short)books[i].PositionId);
                Language language = languageDAO.GetById((short)books[i].LanguageId);
                BookEntity book = new BookEntity();
                book.CategoryId = category.CategoryId;
                book.BookId= books[i].BookId;
                book.Title = books[i].Title;
                book.Author = books[i].Author;
                book.Publisher = books[i].Publisher;
                book.Description = books[i].Description;
                if(images.Count < 1) 
                    book.Images = new List<Image>();
                else
                    book.Images = images;
                book.PageNumber = books[i].PageNumber;
                book.PositionId = books[i].PositionId;
                book.LanguageId= books[i].LanguageId;
                book.Language = language.Name;
                book.Price = books[i].Price;
                book.Position = position;
                book.Status = books[i].Status;
                book.CategoryName = category.Name;
                list.Add(book);
            }
            return list;
        }
    }
}
