// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Conversations
{
	using System;
	using System.Collections.Generic;

	public class Conversation<TAuthorKey>
	{
		internal const string CommentsFieldName = nameof(comments);
		internal const int KeyMaxLength = 80;
		private readonly List<Comment<TAuthorKey>> comments = new List<Comment<TAuthorKey>>();

		public Conversation() : this(null)
		{
		}

		public Conversation(string key)
		{
			this.ChangeKey(key);
			this.CreatedOn = DateTime.UtcNow;
		}

		public IEnumerable<Comment<TAuthorKey>> Comments => this.comments.AsReadOnly();
		public DateTime CreatedOn { get; private set; }
		public int Id { get; private set; }
		public string Key { get; private set; }

		public Comment<TAuthorKey> AddComment(TAuthorKey authorId, string text)
		{
			var comment = new Comment<TAuthorKey>(this, authorId, text);

			this.comments.Add(comment);

			return comment;
		}

		public void ChangeKey(string key)
		{
			key.EnforceMaxLength(KeyMaxLength);
			this.Key = key;
		}
	}
}