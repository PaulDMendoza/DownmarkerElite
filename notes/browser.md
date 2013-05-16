URL resolution issues
=====================

The System.Windows.Forms.WebBrowser control has some limitations:

1. Cannot set the base URL of a document to simulate another location.
2. Cannot intercept URL resolution for embedded resources like images.
3. Embedded resources with relative links or absolute file:/// links do not work if there is no proper base URL.
4. Navigation events to absolute file:/// links never occur if there is no proper base URL.

For documents loaded via the WebBrowser.DocumentText or by modifying the
WebBrowser.Document property, the base URL is "about:empty". This results in two problems for such documents:

1. embedded images (or other resources) with relative or absolute "file:" URIs do not work properly.
2. clickable links with absolute "file" URLs do not work.

To work around these problems, downmarker writes a temporary HTML file in the
system temp path. This temporary file is then loaded by the WebBrowser control.
Any relative links are rewritten to absolute links so that they work in the
temp path.
