namespace SalasDeReuniaoCRUD.Domain.Core.Model
{
    public abstract class Entity<TId> where TId : notnull
    {
        public TId Id { get; set; }
        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity<TId> a, Entity<TId> b) =>
             !(a == b);
        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity<TId>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return true;

            return Id.Equals(compareTo.Id);
        }
        public override int GetHashCode() =>
            (GetType().GetHashCode() * 907) + Id.GetHashCode();
        public override string ToString() =>
            $"{GetType().Name} [Id={Id}]";
    }
}
