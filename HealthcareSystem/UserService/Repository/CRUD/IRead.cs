namespace UserService.Repository.CRUD
{
    public interface IRead<T, K> : IReadCollection<T>
    {
        T Get(K id);
    }
}
