﻿using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface ITopicService
    {
        IList<Topic> GetTopics(int categoryId);
        TopicWithComments? GetTopicWithComments(int id);
        Topic? GetTopic(int id);
        void EditTopic(TopicVM topic);
        void DeleteTopic(Topic topic);
    }
}