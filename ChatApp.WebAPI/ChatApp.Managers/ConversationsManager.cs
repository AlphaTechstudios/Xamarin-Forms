using ChatApp.Managers.Common;
using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Managers
{
    public class ConversationsManager : BaseManager, IConversationsManager
    {
        private readonly IConversationsRepository conversationsRepository;
        private readonly IConversationRepliesRepository conversationRepliesRepository;

        public ConversationsManager(IUnitOfWork unitOfWork,
            IConversationsRepository conversationsRepository,
            IConversationRepliesRepository conversationRepliesRepository)
            : base(unitOfWork)
        {
            this.conversationsRepository = conversationsRepository;
            this.conversationRepliesRepository = conversationRepliesRepository;
        }

        public IEnumerable<ConversationModel> GetAllConversationsByUserId(long userId)
        {
            return conversationsRepository.Get(x => x.UserOneID == userId || x.UserTwoID == userId, includeProperties: "ConversationReplies");
        }

        public ConversationModel GetConversationByUsersId(long firstUser, long secondUser)
        {
            return conversationsRepository.Get(x => (x.UserOneID == firstUser && x.UserTwoID == secondUser) || (x.UserOneID == secondUser && x.UserTwoID == firstUser), includeProperties: "ConversationsReplies").SingleOrDefault();
        }
        public long AddOrUpdateConversation(long firstUser, long secondUser)
        {
            var now = DateTime.UtcNow;
            var conversation = GetConversationByUsersId(firstUser, secondUser);
            if (conversation == null)
            {
                conversation = new ConversationModel
                {
                    UserOneID = firstUser,
                    UserTwoID = secondUser,
                    CreationDate = now,
                    ModificationDate = now
                };

               conversationsRepository.Insert(conversation);
             
            }
            else
            {
                conversation.ModificationDate = DateTime.Now;
                conversationsRepository.Update(conversation);
            }

            UnitOfWork.Commit();
            return conversation.ID;
        }
        public void AddReply(string message, long conversationId, long userID)
        {
            conversationRepliesRepository.Insert(new ConversationReplyModel
            {
                Content = message,
                ConversationID = conversationId,
                CreationDate = DateTime.Now,
                SenderUserId = userID
            });
            UnitOfWork.Commit();
        }

    }
}
