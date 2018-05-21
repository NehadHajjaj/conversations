using System.Collections.Generic;
using Conversations.Core.Domain;

namespace Conversations.Core.Repositories
{
    public interface IConversationsRepository
    {
        ConversationData AddConversation(int conversationId);
        ConversationData GetConversation(string key);
		ConversationData GetConversation(int conversationId);
		IEnumerable<ConversationData> GetOpenedConversations();
        CommentData GetComment(int commentId);
        IEnumerable<ConversationData> GetAllConversations();
		void RemoveConversationDocument(int docId);
		void RemoveComment(int commentId);
        void ArchiveConversation(int conversationId, int userId);
        void UnArchiveConversation(int conversationId);
        CommentData AddConversationComment(string key, int userId, string text, List<int> documentIds, int? parentCommentId);
		void DeleteConversation(int conversationId);
    }
}

