namespace PasswordTextBoxControl
{
    /// <summary>
    /// A function delegate with no arguments.
    /// </summary>
    /// <typeparam name="TResult">The type of result.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public delegate TResult Func<out TResult>();

    /// <summary>
    /// A function delegate with one argument.
    /// </summary>
    /// <typeparam name="TArgument">The type of
    /// <paramref name="argument"/>.</typeparam>
    /// <typeparam name="TResult">The type of result.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public delegate TResult Func<in TArgument, out TResult>(TArgument argument);

    /// <summary>
    /// A function delegate with one argument.
    /// </summary>
    /// <typeparam name="TArgument1">The type of
    /// <paramref name="argument1"/>.</typeparam>
    /// <typeparam name="TArgument2">The type of
    /// <paramref name="argument2"/>.</typeparam>
    /// <typeparam name="TResult">The type of result.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public delegate TResult Func<in TArgument1,
                                 in TArgument2,
                                 out TResult>(TArgument1 argument1,
                                              TArgument2 argument2);

    /// <summary>
    /// A function delegate with one argument.
    /// </summary>
    /// <typeparam name="TArgument1">The type of
    /// <paramref name="argument1"/>.</typeparam>
    /// <typeparam name="TArgument2">The type of
    /// <paramref name="argument2"/>.</typeparam>
    /// <typeparam name="TArgument3">The type of
    /// <paramref name="argument3"/>.</typeparam>
    /// <typeparam name="TResult">The type of result.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public delegate TResult Func<in TArgument1,
                                 in TArgument2,
                                 in TArgument3,
                                 out TResult>(TArgument1 argument1,
                                              TArgument2 argument2,
                                              TArgument3 argument3);
}
