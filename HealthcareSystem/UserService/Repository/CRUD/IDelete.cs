namespace UserService.Repository.CRUD
{
    public interface IDelete<T>
    {
        void Delete(T entity);
    }
}
