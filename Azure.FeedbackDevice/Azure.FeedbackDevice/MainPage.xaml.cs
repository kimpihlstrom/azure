using Azure.FeedbackDevice.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Azure.FeedbackDevice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static String VOTE_BUTTON_PREFIX = "btnVote";
        private static String EVENT_HUB_CONNECTION_STRING = "";
        private static String EVENT_HUB_NAME = "satisfacation";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnVote_Click(object sender, RoutedEventArgs e)
        {
            var eventHub = new EventHub();
            eventHub.ConnectionString = EVENT_HUB_CONNECTION_STRING;

            if (sender is Control)
            {
                Control ctrl = sender as Control;

                if (ctrl.Name.Contains(VOTE_BUTTON_PREFIX))
                {
                    int vote = int.Parse(ctrl.Name.Replace(VOTE_BUTTON_PREFIX, ""));

                    eventHub.Send(EVENT_HUB_NAME, new MessageEntity()
                    {
                        DeviceId = "1",
                        Vote = vote.ToString()
                    });
                }
            }
        }
    }
}
