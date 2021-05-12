﻿using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Net;
using TechshopService.Core.Notifications;
using Xunit;

namespace TechshopService.Core.Test.Notifications
{
    [Trait("Unit", nameof(Notification))]
    public class NotificationTest
    {
        [Theory, AutoData]
        public void Add_GivenMessageAndStatusCode_ThenAddToNotificationList(
            string message,
            HttpStatusCode statusCode)
        {
            // Arrange
            var notification = new Notification();

            // Act
            notification.Add(message, (int)statusCode);

            // Assert
            notification.Any().Should().BeTrue();
        }

        [Theory, AutoData]
        public void Add_GivenException_ThenAddExceptionToNotificationList(
            Exception exception,
            HttpStatusCode statusCode)
        {
            // Arrange
            var notification = new Notification();

            // Act
            notification.Add(exception, (int)statusCode);

            // Assert
            notification.Any().Should().BeTrue();
        }
    }
}
