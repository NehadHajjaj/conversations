using System;
using System.Collections.Generic;
using System.Linq;
using Conversations.Core.Repositories;

namespace Conversations.Core.Domain
{
    public class Conversation<T>
	{
		private Conversation()
		{
		}

		public DateTime? ArchivedOn { get; private set; }
		public ICollection<Comment<T>> Comments { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public int Id { get; private set; }
		public string Key { get; private set; }
	    public int? ArchivedByUserId { get; private set; }

        internal static Conversation<T> Load(ConversationData data, IUserRepository<T> userRepository)
		{
			var conversation = new Conversation<T>
			{
				Id = data.Id,
				CreatedOn = data.CreatedOn,
				Key = data.Key,
				ArchivedOn = data.ArchivedOn,
			    ArchivedByUserId = data.ArchivedByUserId
            };

			conversation.Comments = data.Comments.Select(c => Comment<T>.Load(c, conversation, userRepository)).ToList();

			return conversation;
		}
	}
}