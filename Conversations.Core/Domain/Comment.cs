using System;
using System.Collections.Generic;
using System.Linq;
using Conversations.Core.Repositories;

namespace Conversations.Core.Domain
{
    public class Comment<T>
    {
        private Comment()
        {
        }

        public T Author { get; private set; }
        public int AuthorId { get; private set; }
        public ICollection<Comment<T>> Children { get; private set; }
        public ICollection<ConversationDocument> Documents { get; private set; }
        public Conversation<T> Conversation { get; private set; }
        public int ConversationId { get; private set; }
        public int Id { get; private set; }
        public int? ParentId { get; private set; }
        public DateTime PostedOn { get; private set; }
        public string Text { get; private set; }
        public Guid CorrelationId { get; private set; }

        internal static Comment<T> Load(CommentData data, Conversation<T> conversation,
            IUserRepository<T> userRepository)
        {
            var comment = new Comment<T>
            {
                Id = data.Id,
                Conversation = conversation,
                ConversationId = data.ConversationId,
                ParentId = data.ParentId,
                PostedOn = data.PostedOn,
                Text = data.Text,
                AuthorId = data.AuthorId,
                Author = userRepository.GetUser(data.AuthorId),
                CorrelationId = data.CorrelationId,
                Documents = data.ConversationDocuments
            };

            if (data.Children != null)
                comment.Children = data.Children.Select(c => Load(c, conversation, userRepository)).ToList();

            return comment;
        }

        internal static Comment<T> Load(CommentData data, IUserRepository<T> userRepository)
        {
            var comment = new Comment<T>
            {
                Id = data.Id,
                ConversationId = data.ConversationId,
                ParentId = data.ParentId,
                PostedOn = data.PostedOn,
                Text = data.Text,
                AuthorId = data.AuthorId,
                Author = userRepository.GetUser(data.AuthorId),
                CorrelationId = data.CorrelationId,
                Documents = data.ConversationDocuments
            };

            if (data.Children != null) comment.Children = data.Children.Select(c => Load(c, userRepository)).ToList();

            return comment;
        }
    }
}