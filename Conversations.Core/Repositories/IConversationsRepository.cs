using System.Collections.Generic;
using Conversations.Core.Domain;

namespace Conversations.Core.Repositories
{
    public interface IConversationsRepository
    {
        ConversationData AddConversations(ConversationData conversation);
        ConversationData GetConversation(object id);
        IEnumerable<ConversationData> GetOpenedConversations();
        CommentData GetComment(int commentId);
        IEnumerable<ConversationData> GetAllConversations();
        int RemoveConversationDocuments(ConversationDocument doc);
        int RemoveComments(CommentData item);
        int ArchiveConversation(ConversationData conversation, int userId);
        int DeleteConversationDocuments(int conversationId, int userId);
        int UnArchiveConversation(int conversationId);

        CommentData AddConversationComment(object conversationId, int userId, string text,
            List<ConversationDocument> documents,
            int? replyOnCommentId);

        int DeleteConversation(object key);
    }
}