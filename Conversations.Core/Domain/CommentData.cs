using System;
using System.Collections.Generic;

namespace Conversations.Core.Domain
{
    public class CommentData : IEntity<CommentIdentifier, int>
	{
		public CommentData(int conversationId, int authorId, string text,List<ConversationDocument>documents, int? parentCommentId = null) : this()
		{
			this.ConversationId = conversationId;
			this.AuthorId = authorId;
			this.Text = text;
			this.ParentId = parentCommentId;
			this.PostedOn = DateTime.UtcNow;
			this.CorrelationId = Guid.NewGuid();
		    this.ConversationDocuments = documents;

		}

		private CommentData()
		{
			this.Children = new List<CommentData>();
		}

		public CommentIdentifier Identifier => new CommentIdentifier(this.Id);

		public int AuthorId { get; private set; }
		public virtual ICollection<CommentData> Children { get; private set; }
	    public virtual ICollection<ConversationDocument> ConversationDocuments { get; private set; }
        public virtual ConversationData ConversationData { get; private set; }
		public int ConversationId { get; private set; }
		public int Id { get; private set; }
		public int? ParentId { get; private set; }
		public DateTime PostedOn { get; private set; }
		public string Text { get; private set; }
		public Guid CorrelationId { get; private set; }
	}
}