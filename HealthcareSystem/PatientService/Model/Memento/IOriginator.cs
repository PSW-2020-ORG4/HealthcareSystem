namespace PatientService.Model.Memento
{
    public interface IOriginator<T> where T : IMemento
    {
        T GetMemento();
    }
}
