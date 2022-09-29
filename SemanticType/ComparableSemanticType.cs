using System;

namespace SemanticType
{
  public class ComparableSemanticType<T> :
      SemanticType<T>, IEquatable<SemanticType<T>>, IComparable<ComparableSemanticType<T>>
      where T : IComparable<T>
  {
    protected ComparableSemanticType(Func<T, bool> isValidLambda, T value)
        : base(isValidLambda, value)
    {
    }

    public int CompareTo(ComparableSemanticType<T> other)
    {
      if (other == null) { return 1; }
      return this.Value.CompareTo(other.Value);
    }
  }
}
