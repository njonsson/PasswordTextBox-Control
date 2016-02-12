# _PasswordTextBox_ Control

[![‘PasswordTextBox-Control’ NuGet package]][NuGet-package]

> See what’s changed lately by reading the [project history][project-history].

It’s common knowledge that the
[_UseSystemPasswordChar_][WinForms-UseSystemPasswordChar-property] and
[_PasswordChar_][WinForms-PasswordChar-property] properties of the
_System​.Windows​.Forms​.TextBox_ control let you obscure user input of secret
information such as passwords. But you may have wanted behavior such as
touch-screen devices exhibit: user input is **momentarily displayed before being
replaced** by a password character. _TextBox_ can’t do that.

_PasswordTextBox_ is what you were looking for.

![PasswordTextBox control animated demonstration]

## Features

- [x] Accept user input via the keyboard, the clipboard, and drag-and-drop
- [x] Prevent the user or system from copying control content to the clipboard if
      a password character is in effect
- [x] Show input just once before replacing it with the password character, never
      allowing any input to be revealed again after it has been obscured
- [x] Support the use of the system-defined password character
- [x] Support the use of a custom password character
- [x] Support the use of no password character at all (which makes the control
      behave like a standard text box)
- [x] Support variable delay in connection with the password character
- [x] Observe system settings governing text quality (i.e., antialiasing)
- [x] Support user input via the keyboard by momentarily displaying each typed
      character
- [ ] Support user input via the clipboard by momentarily displaying pasted
      content
- [ ] Support user input via drag-and-drop by momentarily displaying dropped
      content

## Installation

Install [the NuGet package][NuGet-package]. You can do this using the Visual
Studio NuGet plugin, or from the command line in your project folder:
`nuget install PasswordTextBox-Control`.

Using the Visual Studio Windows Forms Designer is optional, and configuring it
requires some manual steps. Once _PasswordTextBoxControl.dll_ is a reference of
your project, you need to add the control to the Visual Studio Toolbox so it can
be dropped on forms in the designer:

1. Click **Choose Toolbox Items…** in the **Tools** menu.
2. On the **.NET Framework Components tab** of the **Choose Toolbox Items**
   dialog box, click the **Browse…** button.
3. Navigate to the _packages_ folder of your project, and within that
   find the _PasswordTextBoxControl.dll_ version that corresponds to the .NET
   Framework version you’re using.
4. Click **OK** to save the changes to the toolbox.
5. Look for _PasswordTextBox_ among _Common Controls_ in the toolbox.

## Usage

See the [_PasswordTextBox.Test.Gui_ project][project-GUI-test] in this solution
for a demonstration of the behavior of the control. Salient properties include:

* [_UseSystemPasswordChar_](#usesystempasswordchar-property)
* [_PasswordChar_](#passwordchar-property)
* [_PasswordCharDelay_](#passwordchardelay-property)
* [_Text_](#text-property)

### _UseSystemPasswordChar_ property

If `true`, this property makes user input appear as bullet characters after it
momentarily appears normally. If `false`, then
[_PasswordChar_](#passwordchar-property) takes effect.

This property defaults to `true`.

### _PasswordChar_ property

This property makes user input appear as a specific character after it
momentarily appears normally. The value of this property has no effect unless
[_UseSystemPasswordChar_](#usesystempasswordchar-property) is `false`.


This property defaults to `'\0'` (the null character), which makes the control
behave like a standard text box if
[_UseSystemPasswordChar_](#usesystempasswordchar-property) is `false`.

### _PasswordCharDelay_ property

This property controls the time in milliseconds during which user input appears
normally before appearing as the password character.

This property defaults to _1000_ (i.e., one second).

### _Text_ property

As you might expect, this property stores user input regardless of any password
character that may be in effect.

## Contributing

To submit a patch to the project:

1. [Fork][fork-project] the official repository.
2. Create your feature branch: `git checkout -b my-new-feature`.
3. Commit your changes: `git commit -am 'Add some feature'`.
4. Push to the branch: `git push origin my-new-feature`.
5. Make sure all tests in the project are still passing, and add test coverage
   of new behavior as appropriate.
6. [Create][compare-project-branches] a new pull request.

To release a new version:

1. Update the project history in [_History.md_][project-history], and commit.
2. Update the version numbers in the _PasswordTextBox-Control.nuspec_ file and in
   all _AssemblyInfo.cs_ files, and commit.
3. Build the NuGet package: `nuget pack PasswordTextBox-Control.nuspec`.
4. Publish the NuGet package: `nuget push PasswordTextBox-Control.X.Y.Z.nupkg`
   where `X.Y.Z` is the version you just built.
5. Tag the commit and push commits and tags.

## License

Released under the [MIT License][MIT-License].

[‘PasswordTextBox-Control’ NuGet package]:        https://img.shields.io/nuget/v/PasswordTextBox-Control.svg
[PasswordTextBox control animated demonstration]: https://raw.githubusercontent.com/njonsson/PasswordTextBox-Control/master/demo.gif

[project-history]:                         https://github.com/njonsson/PasswordTextBox-Control/blob/master/History.md                       "‘PasswordTextBox Control’ project history"
[WinForms-UseSystemPasswordChar-property]: https://msdn.microsoft.com/en-us/library/system.windows.forms.textbox.usesystempasswordchar.aspx "‘System​.Windows​.Forms​.TextBox​.UseSystemPasswordChar’ property in the .NET Framework Class Library"
[WinForms-PasswordChar-property]:          https://msdn.microsoft.com/en-us/library/system.windows.forms.textbox.passwordchar.aspx          "‘System​.Windows​.Forms​.TextBox​.PasswordChar’ property in the .NET Framework Class Library"
[NuGet-package]:                           https://www.nuget.org/packages/PasswordTextBox-Control/                                          "‘PasswordTextBox-Control’ NuGet package"
[project-GUI-test]:                        https://github.com/njonsson/PasswordTextBox-Control/tree/master/PasswordTextBoxControl.Test.Gui "‘PasswordTextBox Control’ GUI test"
[fork-project]:                            https://github.com/njonsson/PasswordTextBox-Control/fork                                         "Fork the official repository of ‘PasswordTextBox Control’"
[compare-project-branches]:                https://github.com/njonsson/PasswordTextBox-Control/compare                                      "Compare branches of ‘PasswordTextBox Control’ repositories"
[MIT-License]:                             https://github.com/njonsson/PasswordTextBox-Control/blob/master/License.md                       "MIT License claim for ‘PasswordTextBox Control’"
