Tests
====

Unit Tests
----------------

The DownMarker unit tests are written with [NUnit](http://www.nunit.org) and [Rhino Mocks](http://www.ayende.com/projects/rhino-mocks.aspx).

The DownMarker presentation code follows the [MVVM](http://en.wikipedia.org/wiki/Model_View_ViewModel) pattern. The view model
does modal user interaction through an injected `IPrompt` abstraction. This makes it possible to unit test even user interaction by mocking the `IPrompt` interface.

You can run the unit tests on Windows by running `nunit.bat`. 

Manual Tests
---------------------

Some non-regression tests cannot be automated (easily) because they test things implemented in the view rather than the viewmodel.

* alt+F4 exits
* CTRL+S saves
* line-endings should **not** be transformed from `\r\n` to `\n` (this was an issue with `RichTextBox.Text` which only occurs if the control is actually shown)
* trying to select text by dragging from one line to the line above it does not discard the starting point of the selection in strange ways (this seemed to be caused by two-way binding of the `SelectionStart` and/or `SelectionLength` properties)