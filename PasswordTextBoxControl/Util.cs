using System;
using System.Diagnostics;

namespace PasswordTextBoxControl
{
    /// <summary>
    /// Provides utility static methods.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Writes a stack trace to the <see cref="Debug"/> trace listeners.
        /// </summary>
        [Conditional("DEBUG")]
        public static void DebugStackTrace()
        {
            var stackTrace = new StackTrace().ToString().Split(new[] { Environment.NewLine },
                                                                StringSplitOptions.RemoveEmptyEntries);
            foreach (var f in stackTrace)
            {
                Debug.WriteLine(f);
            }
        }

        /// <summary>
        /// Executes the specified <paramref name="action"/> after indenting the
        /// <see cref="Debug"/> output. Unindents the output after executing the
        /// <paramref name="action"/>.
        /// </summary>
        /// <param name="action">An <see cref="Action"/>.</param>
        public static void DebugWithIndentation(Action action)
        {
            DebugWithIndentation(delegate
            {
                action();
                return false;
            });
        }

        /// <summary>
        /// Executes the specified <paramref name="action"/> after indenting the
        /// <see cref="Debug"/> output. Unindents the output after executing the
        /// <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of return value expected from the
        /// <paramref name="action"/>.</typeparam>
        /// <param name="action">A <see cref="Func{TResult}"/>.</param>
        /// <returns>The <typeparamref name="T"/> return value of the
        /// <paramref name="action"/>.</returns>
        public static T DebugWithIndentation<T>(Func<T> action)
        {
            Debug.Indent();
            try
            {
                return action();
            }
            finally
            {
                Debug.Unindent();
            }
        }
    }
}
