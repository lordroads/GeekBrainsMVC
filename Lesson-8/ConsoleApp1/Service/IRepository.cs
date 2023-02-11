namespace Client.Service;

public interface IRepository<TClass, Tkey>
{
    IList<TClass> GetAll();
    TClass GetById(Tkey id);
    Tkey Create(TClass entity);
    bool Upadte(TClass entity);
    bool Delete(Tkey id);
}