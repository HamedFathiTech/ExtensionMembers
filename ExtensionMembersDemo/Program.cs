
using ExtensionMembersDemo;

var dateOnly = DateTime.Now.ToDateOnly();
var timeOnly = DateTime.Now.ToTimeOnly();

var dateOnly2 = ExtensionMembers.ToDateOnly(DateTime.Now);
var timeOnly2 = ExtensionMembers.ToTimeOnly(DateTime.Now);

int a = 5, b = 10;
a.SwapRef(ref b);
ExtensionMembers.SwapRef<int>(ref a, ref b);

var list = new List<int>();
var status = list.IsEmpty();
var status2 = ExtensionMembers.IsEmpty(list);

var dateOnly3 = DateTime.Now.DatePart;
var timeOnly3 = DateTime.Now.TimePart;

// get_X
ExtensionMembers.get_DatePart(DateTime.Now);
ExtensionMembers.get_TimePart(DateTime.Now);

var fileStream = FileHelper.CreateFileRecursively(@"C:\Users\X\Desktop\aa\bb\cc\sample.txt");

var fileStream2 = File.CreateRecursively(@"C:\Users\X\Desktop\aa\bb\cc\sample.txt");

var status3 = string.HasValue("HamedFathiTech");

var e = double.E;

var fullPath = "part1" / "part2" / "test.txt";

// OLD-style Extensions
public static class Extensions
{
    public static DateOnly ToDateOnly_(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static TimeOnly ToTimeOnly_(this DateTime dateTime)
    {
        return TimeOnly.FromDateTime(dateTime);
    }
    public static bool IsEmpty_<T>(this IEnumerable<T> target) => !target.Any();

    public static void SwapRef_<T>(this ref T left, ref T right) where T : struct
    {
        (left, right) = (right, left);
    }
}

public static class ExtensionMembers
{
    extension(DateTime dateTime)
    {
        public DateOnly ToDateOnly()
        {
            return DateOnly.FromDateTime(dateTime);
        }

        public TimeOnly ToTimeOnly()
        {
            return TimeOnly.FromDateTime(dateTime);
        }

        // Instance Properties
        public DateOnly DatePart => DateOnly.FromDateTime(dateTime);

        public TimeOnly TimePart
        {
            get { return TimeOnly.FromDateTime(dateTime); }
        }
    }

    // Using generics
    extension<T>(IEnumerable<T> collection)
    {
        public bool IsEmpty()
        {
            return !collection.Any();
        }
    }

    // Using ref and generic with constraint
    extension<T>(ref T value) where T : struct
    {
        public void SwapRef(ref T other)
        {
            (value, other) = (other, value);
        }
    }

    extension(File)
    {
        public static FileStream CreateRecursively(string filePath, bool overwrite = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            }
            else
            {
                throw new ArgumentException("Invalid file path: no directory specified.", nameof(filePath));
            }

            FileMode mode = overwrite ? FileMode.Create : FileMode.OpenOrCreate;

            try
            {
                return new FileStream(filePath, mode, FileAccess.ReadWrite, FileShare.None);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to create or open file: {filePath}", ex);
            }
        }
    }

    // Static members for non-static type
    extension(string)
    {
        // Type Methods
        public static bool HasValue(string value) => !string.IsNullOrWhiteSpace(value);

        // Operators
        public static string operator /(string left, string right)
            => Path.Combine(left, right);
    }

    extension(double)
    {
        // Type Properties
        // Euler's number (e), the base of natural logarithms.
        public static double E => 2.718281828459045;
    }
}