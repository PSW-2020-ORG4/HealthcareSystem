namespace UserService.Repository.CRUD
{
    public interface ICreate<T>
    {
        void Add(T entity);
    }
}
