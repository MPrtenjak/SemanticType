using SemanticType.Interfaces;
using System;

namespace SemanticType
{
  public abstract class SemanticType<T> : IEquatable<SemanticType<T>>, IValue<T>
  {
    public T Value { get; private set; }

    protected SemanticType(Func<T, bool> isValidLambda, T value)
    {
      if ((object)value == null)
      {
        throw new ArgumentException(string.Format("Trying to use null as the value of a {0}", this.GetType()));
      }

      if ((isValidLambda != null) && !isValidLambda(value))
      {
        throw new ArgumentException(string.Format("Trying to set a {0} to {1} which is invalid", this.GetType(), value));
      }

      Value = value;
    }

    public override bool Equals(Object obj)
    {
      //Check for null and compare run-time types. 
      if (obj == null || obj.GetType() != this.GetType())
      {
        return false;
      }

      return (Value.Equals(((SemanticType<T>)obj).Value));
    }

    public override int GetHashCode()
    {
      return Value.GetHashCode();
    }

    public virtual bool Equals(SemanticType<T> other)
    {
      if (other == null) { return false; }

      return (Value.Equals(other.Value));
    }

    public static bool operator ==(SemanticType<T> a, SemanticType<T> b)
    {
      // If both are null, or both are same instance, return true.
      if (System.Object.ReferenceEquals(a, b))
      {
        return true;
      }

      // If one is null, but not both, return false.
      // Have to cast to object, otherwise you recursively call this == operator.
      if (EitherNull(a, b))
      {
        return false;
      }

      // Return true if the fields match:
      return a.Equals(b);
    }

    public static bool operator !=(SemanticType<T> a, SemanticType<T> b)
    {
      return !(a == b);
    }

    protected static bool EitherNull(SemanticType<T> a, SemanticType<T> b)
    {
      return (a is null) || (b is null);
    }

    public override string ToString()
    {
      return this.Value.ToString();
    }
  }
}
