# WordPress Database Importer

This is a project I've used to import posts directly into the `posts` table of a WordPress database. It doesn't handle every case, but should work for basic blog posts.

## Using this importer

Start with the `WordPressDatabaseImport` class, providing:

- A writer object (see below)
- The timezone modifier for your site
- The base URL for your site, including trailing slash
- The author ID that posts should be imported under

The writer object should typically be `DbWordPressDataWriter`, which can be provided a .NET ```DbConnection``` and the table prefix (e.g., "wp\_" for default WordPress installs.)

When creating a post, call the following fluent methods on PostBuilder to add data:

- `SetStatus` can set the status of the post (usually "draft" or "publish", but can be "future" for schedule posts with a future post\_date).
- `SetPostExcerpt` can set the post excerpt field.
- `SetContent` sets the HTML content. You can use the `WordPressBlock` class to build content that works with the "blocks" system, or dump in any HTML.
- `SetPostDate` sets the post date. For scheduled posts, you can set this in the future.
- Calling `InsertPost` will write the post to the database.

I suggest backing up your database before running this, so that you can test the results.

## Using the block builder

You can use `WordPressBlock` to build post content in HTML blocks. This doesn't support everything, just the few use cases I needed, including:

- `CreateTextNode` creates a snippet of plain HTML text, which can be added to blocks as a child.
- `CreateParagraph` creates a paragraph block containing the given HTML.
- `CreateHeading` creates a heading of the given level with the given blocks or text snippets as contents.
- `CreateUnorderedList` creates an unordered list with the given blocks as children (shoudl be list items)
- `CreateListItem` creates a list item with the given blocks or text snippets as children.
- `CreateBlockQuote` creates a blockquote with the given blocks or text snippets as children.
- `CreateImage` creates an image iwth the given properties.

WordPress can be finicky about block HTML that doesn't conform to the expected format, so you may want to test the results in a dev environment before doing your final import.