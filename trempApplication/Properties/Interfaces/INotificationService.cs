﻿namespace trempApplication.Properties.Interfaces
{
    public interface INotificationService
    {
        Task<string> sendNotification(string registrationToken);
    }
}
