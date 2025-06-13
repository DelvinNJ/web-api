using WebApi.Entity;

namespace WebApi.Repository
{
    public class PageResult<T>
    {
        public List<Student> Items { get; internal set; }
    }
}