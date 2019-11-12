using System;

namespace Audio_sampler.Player
{
    public static class Indexes
    {
        public const int DisplayPageSize = 9;
        public const int PageSections = 4;
        public const int PageSize = DisplayPageSize * PageSections;

        internal static int ValueFromIndex(int index)
        {
            return index + 1;
        }

        internal static int IndexFromValue(int value)
        {
            return value - 1;
        }
    }

    public struct ButtonValue
    {
        public static ButtonValue First = new ButtonValue(1);
        public static ButtonValue LastPageExclusive = new ButtonValue(First.RawValue + Indexes.DisplayPageSize);
        public static ButtonValue LastOverallExclusive = new ButtonValue(First.RawValue + Indexes.PageSize);

        public readonly int RawValue;

        private ButtonValue(int rawValue)
        {
            RawValue = rawValue;
        }

        public static ButtonValue FromRaw(int rawValue)
        {
            if (First.RawValue <= rawValue && rawValue <= LastOverallExclusive.RawValue)
                return new ButtonValue(rawValue);

            var msg =
                $"Condition {First.RawValue} <= rawValue <= {LastOverallExclusive.RawValue} failed; rawValue = {rawValue}";

            throw new ArgumentException(msg);
        }

        public static ButtonValue From(SampleIndex index)
        {
            return FromRaw(Indexes.ValueFromIndex(index.RawIndex));
        }

        public SampleIndex SampleIndex => SampleIndex.From(this);

        public static ButtonValue operator +(ButtonValue value, int shift)
        {
            return FromRaw(value.RawValue + shift);
        }

        public static bool operator ==(ButtonValue a, ButtonValue b)
        {
            return a.RawValue == b.RawValue;
        }

        public static bool operator !=(ButtonValue a, ButtonValue b)
        {
            return !(a == b);
        }

        public static bool operator <(ButtonValue a, ButtonValue b)
        {
            return a.RawValue < b.RawValue;
        }

        public static bool operator <=(ButtonValue a, ButtonValue b)
        {
            return a < b || a == b;
        }

        public static bool operator >(ButtonValue a, ButtonValue b)
        {
            return b < a;
        }

        public static bool operator >=(ButtonValue a, ButtonValue b)
        {
            return b <= a;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            return obj is ButtonValue other && this == other;
        }

        public override int GetHashCode()
        {
            return RawValue.GetHashCode();
        }
    }

    public struct SampleIndex
    {
        public static SampleIndex First = new SampleIndex(0);

        public readonly int RawIndex;

        private SampleIndex(int rawIndex)
        {
            RawIndex = rawIndex;
        }

        private static SampleIndex FromRaw(int rawIndex)
        {
            if (rawIndex < First.RawIndex)
                throw new ArgumentOutOfRangeException($"rawIndex cannot be < {First.RawIndex}; got {rawIndex}");

            return new SampleIndex(rawIndex);
        }

        public static SampleIndex From(ButtonValue value)
        {
            return FromRaw(Indexes.IndexFromValue(value.RawValue));
        }

        public ButtonValue ButtonValue => ButtonValue.From(this);

        public static SampleIndex operator +(SampleIndex value, int shift)
        {
            return FromRaw(value.RawIndex + shift);
        }

        public static bool operator ==(SampleIndex a, SampleIndex b)
        {
            return a.RawIndex == b.RawIndex;
        }

        public static bool operator !=(SampleIndex a, SampleIndex b)
        {
            return !(a == b);
        }

        public static bool operator <(SampleIndex a, SampleIndex b)
        {
            return a.RawIndex < b.RawIndex;
        }

        public static bool operator <=(SampleIndex a, SampleIndex b)
        {
            return a < b || a == b;
        }

        public static bool operator >(SampleIndex a, SampleIndex b)
        {
            return b < a;
        }

        public static bool operator >=(SampleIndex a, SampleIndex b)
        {
            return b <= a;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            return obj is SampleIndex other && this == other;
        }

        public override int GetHashCode()
        {
            return RawIndex.GetHashCode();
        }
    }
}