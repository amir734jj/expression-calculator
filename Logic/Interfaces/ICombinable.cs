namespace Logic.Interfaces
{
    public interface ICombinable<T> where T: ICombinable<T>
    {
        public T Combine(T other);
    }
}