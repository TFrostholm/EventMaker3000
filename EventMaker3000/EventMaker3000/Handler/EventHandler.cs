using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using EventMaker3000.Converter;
using EventMaker3000.ViewModel;

namespace EventMaker3000.Handler
{
    public class EventHandler
    {

        // THIS IS A BIT LIKE LARMANS CONTROLLER PARTERN

        // Reference to EventViewModel. 
        public EventViewModel EventViewModel { get; set; }

        // Constructor: Initializes the reference. Made by typing ctrop tab
        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel = eventViewModel;
        }

        public void CreateEvent()
        {
            EventViewModel.EventCatalogSingleton.AddEvent(EventViewModel.Id, EventViewModel.Name, EventViewModel.Description, EventViewModel.Place, DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventViewModel.Date, EventViewModel.Time));
        }


        private void DeleteEvent()
        {
            EventViewModel.EventCatalogSingleton.Events.Remove(EventViewModel.SelectedEvent);
        }


        // Confirmation box that prompts user before deletion
        public async void GetDeleteConfirmationAsync()
        {
            MessageDialog msgbox = new MessageDialog("Are you sure you want to permenantly delete this event?", "Delete event");

            msgbox.Commands.Add(new UICommand
            {
                Label = "Yes",
                Invoked = command => DeleteEvent()
            }
            );

            msgbox.Commands.Add(new UICommand
            {
                Label = "No",
            }
            );
            msgbox.DefaultCommandIndex = 1;
            msgbox.CancelCommandIndex = 1;
            msgbox.Options = MessageDialogOptions.AcceptUserInputAfterDelay;

            await msgbox.ShowAsync();
        }

        public void EnableOrNot()
        {
            EventViewModel.DeletebuttonEnableOrNot = EventViewModel.DeletebuttonEnableOrNot = true;
        }

        //public void TextBlockVisibility()
        //{
        //    if (EventViewModel.EventCatalogSingleton.Events.Count < 1)
        //    {
        //        EventViewModel.TextBlockVisibility = EventViewModel.TextBlockVisibility = "Visible";
        //    }        
        //}

    }
}