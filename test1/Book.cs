using static System.Reflection.Metadata.BlobBuilder;
using System.Text;

namespace test1
{
    public class Book
    {
        public string Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public long DateOfPublication { get; private set; }
        public Book(string id, string title, string author, long dateOfPublication)
        {
            this.Id = id;
            this.Title = title;
            this.Author = author;
            this.DateOfPublication = dateOfPublication;
              
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Book;
            if (other == null) return false;
            return this.Id == other.Id;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public void copy(Book book)
        {
            Author = book.Author;
            Title = book.Title;
            DateOfPublication = book.DateOfPublication;
        }


        public static string Valid(Book book)
        {

            
            StringBuilder error = new StringBuilder();
            if(book.Id == "")
                error.Append("Id field should not be empty\n");
            if (book.DateOfPublication > ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds())
                error.Append("Too new (dateOfPublicion field)\n");
            if (book.Author == null || book.Author.Length == 0)
                error.Append("Author field should not be empty\n");
            if (book.Title == null || book.Title.Length == 0)
                error.Append("Tittle field should not be empty\n");
            return error.ToString();
        }
    }
}
