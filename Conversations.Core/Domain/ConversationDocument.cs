namespace Conversations.Core.Domain
{
    public class ConversationDocument
    {
        public virtual CommentData CommentData { get; set; }
        public int? CommentDataId { get; set; }
        public int DocumentId { get; set; }
        public int Id { get; set; }
    }
}