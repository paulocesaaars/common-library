using Deviot.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Deviot.CommonTests
{
    [ExcludeFromCodeCoverage]
    public class NotifierTest
    {
        private INotifier _notifier;

        public NotifierTest()
        {
            _notifier = new Notifier(null);
            _notifier.NotifySuccess("sucesso");
            _notifier.NotifyValidationError("erro de validação");
            _notifier.NotifyInternalError("erro interno");
            _notifier.NotifyExternalError("erro externo");
        }

        [Fact(DisplayName = "Verifica se tem notificações")]
        public void HasNotifications()
        {
            Assert.True(_notifier.HasNotifications());
        }

        [Theory(DisplayName = "Verifica se tem notificações - Filtro")]
        [InlineData(NotificationTypeEnum.Success)]
        [InlineData(NotificationTypeEnum.ValidationError)]
        [InlineData(NotificationTypeEnum.InternalError)]
        [InlineData(NotificationTypeEnum.ExternalError)]
        public void HasNotifications_Filter(NotificationTypeEnum type)
        {
            Assert.True(_notifier.HasNotifications(type));
        }

        [Fact(DisplayName = "Retorna todas as notificações")]
        public void GetNotifications()
        {
            var notifications = _notifier.GetNotifications();
            Assert.True(notifications.Count() == 4);
        }

        [Theory(DisplayName = "Retorna somente do filtro")]
        [InlineData(NotificationTypeEnum.Success)]
        [InlineData(NotificationTypeEnum.ValidationError)]
        [InlineData(NotificationTypeEnum.InternalError)]
        [InlineData(NotificationTypeEnum.ExternalError)]
        public void GetNotifications_Filter(NotificationTypeEnum type)
        {
            var notifications = _notifier.GetNotifications(type);
            Assert.True(notifications.Count() == 1);
        }
    }
}
