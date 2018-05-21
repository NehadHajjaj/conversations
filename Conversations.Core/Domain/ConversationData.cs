using System;
using System.Collections.Generic;
using Conversations.Core.Exceptions;

namespace Conversations.Core.Domain
{
    public class ConversationData : IEntity<ConversationIdentifier, int>
	{
		public ConversationData()
		{
			this.Comments = new List<CommentData>();
		}

		public ConversationData(string key) : this()
		{
			this.Key = key;
			this.CreatedOn = DateTime.UtcNow;
		}
		
		public virtual ICollection<CommentData> Comments { get; private set; }

		public ConversationIdentifier Identifier => new ConversationIdentifier(this.Id);

		public string Key { get; private set; }

		public string Reference => GetReference(this.Key);

		public DateTime? ArchivedOn { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public int Id { get; private set; }
	    public int? ArchivedByUserId { get; private set; }

        public static string GetReference(string key)
		{
			var normalizedKey = key.Replace("[", "(").Replace("]", ")").Replace(" ", "-").Replace("--", "-");
			return $"[{normalizedKey}]";
		}

		internal static object GetReference(int id)
		{
			return $"#{id}";
		}

		public CommentData AddComment(int userId, string text,List<ConversationDocument> documents, int? parentCommentId)
		{
			if (this.ArchivedOn != null && this.ArchivedOn <= DateTime.UtcNow)
			{
				var message = $"Conversation {this.Reference} has already been archived. Further comments are not allowed.";
				throw new ConversationException(message);
			}

			var comment = new CommentData(this.Id, userId, text, documents, parentCommentId);

			this.Comments.Add(comment);

			return comment;
		}

		public void Archive(int userId)
		{
			this.ArchivedOn = DateTime.UtcNow;
		    this.ArchivedByUserId = userId;
		}

	    public void UnArchive()
	    {
	        this.ArchivedOn = null;
	        this.ArchivedByUserId = null;
	    }
    }
}