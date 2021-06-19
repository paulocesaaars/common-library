using Deviot.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
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
            _notifier.Notify(HttpStatusCode.OK, "teste");
        }

        [Fact(DisplayName = "Verifica se tem notificações")]
        public void HasNotifications()
        {
            Assert.True(_notifier.HasNotifications);
        }

        [Fact(DisplayName = "Retorna todas as notificações")]
        public void GetNotifications()
        {
            var notifications = _notifier.GetNotifications();
            Assert.True(notifications.Any());
        }
    }
}
